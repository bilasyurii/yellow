namespace Yellow.Core.States
{
    public abstract class State : IState
    {
        public abstract void OnEnter();

        public abstract void OnLeave();

        public abstract void Update();

        public abstract void FixedUpdate();
    }
}
