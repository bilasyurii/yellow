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

        public float value;

        public int raw;

        public Key negative;

        public Key positive;

        public Key alternativeNegative;

        public Key alternativePositive;
    }
}
