using static SFML.Window.Keyboard;

namespace Yellow.Core.InputManagement
{
    public class InputAxis
    {
        public enum State
        {
            None,
            Negative,
            Positive,
            Both,
        };

        public State state = State.None;

        public bool changing = false;

        public int raw;

        public float value;

        public float sensitivity = 3f;

        public Key negative;

        public Key positive;

        public Key alternativeNegative;

        public Key alternativePositive;
    }
}
