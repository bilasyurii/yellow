using SFML.Window;
using System;
using System.Collections.Generic;
using Yellow.Core.ScreenManagement;
using static SFML.Window.Keyboard;

namespace Yellow.Core.InputManagement
{
    public class Input
    {
        private readonly Screen screen;

        private readonly Dictionary<string, InputAxis> axises = new Dictionary<string, InputAxis>();

        private bool[] previousStates;

        private bool[] currentStates;

        private readonly int keyCount;

        private readonly Dictionary<string, int> keyNames;

        public event EventHandler<KeyEventArgs> KeyDown;

        public event EventHandler<KeyEventArgs> KeyUp;

        public Input(Screen screen)
        {
            this.screen = screen;

            keyCount = (int)Key.KeyCount;

            previousStates = new bool[keyCount];
            currentStates = new bool[keyCount];
            keyNames = new Dictionary<string, int>(keyCount);

            InitEventHandlers();
            GenerateKeyNames();
        }

        public void Update()
        {
            var temp = previousStates;
            previousStates = currentStates;
            currentStates = temp;
            Array.Fill(currentStates, false);
        }

        public float Axis(string name)
        {
            return axises[name].value;
        }

        public float AxisRaw(string name)
        {
            return axises[name].raw;
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

        public void AddAxis(string name, Key negative, Key positive)
        {
            axises.Add(name, new InputAxis()
            {
                negative = negative,
                positive = positive
            });

            // TODO add input handlers?
        }

        private void InitEventHandlers()
        {
            var window = screen.Window;

            window.KeyPressed += OnKeyPressed;
            window.KeyReleased += OnKeyReleased;
        }

        private void GenerateKeyNames()
        {
            var keyType = typeof(Key);

            for (var i = 0; i < keyCount; ++i)
            {
                keyNames.Add(Enum.GetName(keyType, i), i);
            }
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            var keyNumber = (int)e.Code;

            currentStates[keyNumber] = true;

            if (!previousStates[keyNumber])
            {
                OnKeyPressed(sender, e);
            }
        }

        private void OnKeyReleased(object sender, KeyEventArgs e)
        {
            var keyNumber = (int)e.Code;

            currentStates[keyNumber] = false;

            // we are sure here, that in previous frame
            // the key was down, because system doesn't
            // send 'up' events multiple times, as it does
            // with key down events
            OnKeyReleased(sender, e);
        }
    }
}
