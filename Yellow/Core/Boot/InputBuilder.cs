namespace Yellow.Core.Boot
{
    public class InputBuilder
    {
        public bool setupDefaultAxises = true;

        public InputBuilder SetupDefaultAxises()
        {
            setupDefaultAxises = true;

            return this;
        }

        public InputBuilder RemoveDefaultAxises()
        {
            setupDefaultAxises = false;

            return this;
        }
    }
}
