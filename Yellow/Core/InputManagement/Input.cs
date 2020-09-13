using SFML.Window;
using System;
using System.Collections.Generic;
using Yellow.Core.Boot;
using Yellow.Core.ScreenManagement;
using Yellow.Core.Utils;
using static SFML.Window.Keyboard;

namespace Yellow.Core.InputManagement
{
    public class Input
    {
        private readonly Screen screen;

        private readonly Dictionary<string, InputAxis> axises = new Dictionary<string, InputAxis>();

        private readonly bool[] previousStates;

        private readonly bool[] currentStates;

        private readonly InputAxis[] axisKeys;

        private readonly int keyCount;

        private readonly Dictionary<string, int> keyNames;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public Input(InputBuilder builder, Screen screen)
        {
            this.screen = screen;

            keyCount = (int)Key.KeyCount;

            previousStates = new bool[keyCount];
            currentStates = new bool[keyCount];
            keyNames = new Dictionary<string, int>(keyCount);
            axisKeys = new InputAxis[keyCount];

            InitEventHandlers();
            GenerateKeyNames();

            if (builder.setupDefaultAxises)
            {
                SetupDefaultAxises();
            }
        }

        public void Update(float dt)
        {
            currentStates.CopyTo(previousStates, 0);

            InputAxis axis;
            float step, difference;

            foreach (var axisItem in axises)
            {
                axis = axisItem.Value;

                if (axis.changing)
                {
                    step = axis.sensitivity * dt;
                    difference = axis.raw - axis.value;

                    if (MathF.Abs(difference) > step)
                    {
                        axis.value += step * Math2.Sign(difference);
                    }
                    else
                    {
                        axis.value = axis.raw;
                        axis.changing = false;
                    }
                }
            }
        }

        public float Axis(string name)
        {
            return axises[name].value;
        }

        public float AxisRaw(string name)
        {
            return axises[name].raw;
        }

        public void SetupAxis(string name, Key negative, Key positive, Key alternativeNegative, Key alternativePositive)
        {
            if (axises.TryGetValue(name, out InputAxis axis))
            {
                axisKeys[(int)axis.negative] = null;
                axisKeys[(int)axis.positive] = null;

                if (axis.alternativeNegative != Key.Unknown)
                {
                    axisKeys[(int)axis.alternativeNegative] = null;
                    axisKeys[(int)axis.alternativePositive] = null;
                }

                axis.negative = negative;
                axis.positive = positive;
                axis.alternativeNegative = alternativeNegative;
                axis.alternativePositive = alternativePositive;
            }
            else
            {
                axis = new InputAxis()
                {
                    negative = negative,
                    positive = positive,
                    alternativeNegative = alternativeNegative,
                    alternativePositive = alternativePositive,
                };

                axises.Add(name, axis);
            }

            axisKeys[(int)negative] = axis;
            axisKeys[(int)positive] = axis;
            axisKeys[(int)alternativeNegative] = axis;
            axisKeys[(int)alternativePositive] = axis;
        }

        public void SetupAxis(string name, Key negative, Key positive)
        {
            if (axises.TryGetValue(name, out InputAxis axis))
            {
                axisKeys[(int)axis.negative] = null;
                axisKeys[(int)axis.positive] = null;

                if (axis.alternativeNegative != Key.Unknown)
                {
                    axisKeys[(int)axis.alternativeNegative] = null;
                    axisKeys[(int)axis.alternativePositive] = null;

                    axis.alternativeNegative = Key.Unknown;
                    axis.alternativePositive = Key.Unknown;
                }

                axis.negative = negative;
                axis.positive = positive;
            }
            else
            {
                axis = new InputAxis()
                {
                    negative = negative,
                    positive = positive,
                    alternativeNegative = Key.Unknown,
                    alternativePositive = Key.Unknown,
                };

                axises.Add(name, axis);
            }

            axisKeys[(int)negative] = axis;
            axisKeys[(int)positive] = axis;
        }

        public void RemoveInputAxis(string name)
        {
            if (axises.TryGetValue(name, out InputAxis axis))
            {
                axisKeys[(int)axis.negative] = null;
                axisKeys[(int)axis.positive] = null;

                if (axis.alternativeNegative != Key.Unknown)
                {
                    axisKeys[(int)axis.alternativeNegative] = null;
                    axisKeys[(int)axis.alternativePositive] = null;
                }

                axises.Remove(name);
            }
        }

        public InputAxis GetInputAxis(string name)
        {
            return axises[name];
        }

        public bool IsKeyPressed(Key key)
        {
            return currentStates[(int)key];
        }

        public bool IsKeyPressed(string key)
        {
            return currentStates[keyNames[key]];
        }

        public bool IsKeyDown(Key key)
        {
            return currentStates[(int)key] && !previousStates[(int)key];
        }

        public bool IsKeyDown(string key)
        {
            var keyNumber = keyNames[key];

            return currentStates[keyNumber] && !previousStates[keyNumber];
        }

        public bool IsKeyUp(Key key)
        {
            return !currentStates[(int)key] && previousStates[(int)key];
        }

        public bool IsKeyUp(string key)
        {
            var keyNumber = keyNames[key];

            return !currentStates[keyNumber] && previousStates[keyNumber];
        }

        private void InitEventHandlers()
        {
            var window = screen.Window;

            window.KeyPressed += OnKeyDown;
            window.KeyReleased += OnKeyUp;
        }

        private void GenerateKeyNames()
        {
            var keyType = typeof(Key);

            for (var i = 0; i < keyCount; ++i)
            {
                keyNames.Add(Enum.GetName(keyType, i), i);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var keyNumber = (int)e.Code;

            if (keyNumber < 0)
            {
                return;
            }

            currentStates[keyNumber] = true;

            if (!previousStates[keyNumber])
            {
                InputAxis axis = axisKeys[keyNumber];

                if (axis != null)
                {
                    axis.changing = true;

                    if (axis.negative == e.Code || axis.alternativeNegative == e.Code)
                    {
                        axis.raw = -1;

                        switch (axis.state)
                        {
                            case InputAxis.State.Positive:
                                axis.state = InputAxis.State.Both;
                                break;

                            case InputAxis.State.None:
                                axis.state = InputAxis.State.Negative;
                                break;
                        }
                    }
                    else
                    {
                        axis.raw = 1;

                        switch (axis.state)
                        {
                            case InputAxis.State.Negative:
                                axis.state = InputAxis.State.Both;
                                break;

                            case InputAxis.State.None:
                                axis.state = InputAxis.State.Positive;
                                break;
                        }
                    }
                }

                KeyDown?.Invoke(sender, e);
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var keyNumber = (int)e.Code;

            if (keyNumber < 0)
            {
                return;
            }

            currentStates[keyNumber] = false;

            InputAxis axis = axisKeys[keyNumber];

            if (axis != null)
            {
                axis.changing = true;

                if (axis.state == InputAxis.State.Both)
                {
                    if (axis.negative == e.Code || axis.alternativeNegative == e.Code)
                    {
                        axis.state = InputAxis.State.Positive;
                        axis.raw = 1;
                    }
                    else
                    {
                        axis.state = InputAxis.State.Negative;
                        axis.raw = -1;
                    }
                }
                else
                {
                    axis.state = InputAxis.State.None;
                    axis.raw = 0;
                }
            }

            // we are sure here, that in previous frame
            // the key was down, because system doesn't
            // send 'up' events multiple times, as it does
            // with key down events
            KeyUp?.Invoke(sender, e);
        }

        private void SetupDefaultAxises()
        {
            SetupAxis("Horizontal", Key.A, Key.D, Key.Left, Key.Right);
            SetupAxis("Vertical", Key.W, Key.S, Key.Up, Key.Down);
        }
    }
}
