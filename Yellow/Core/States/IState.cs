namespace Yellow.Core.States
{
    public interface IState
    {
        void OnEnter();

        void OnLeave();

        void Update();

        void FixedUpdate();
    }
}
