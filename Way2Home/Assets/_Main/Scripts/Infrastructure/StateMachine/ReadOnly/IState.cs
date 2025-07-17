namespace Infrastructure.Services.State
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IPayloadedState<TPayload_1, TPayload_2> : IExitableState
    {
        void Enter(TPayload_1 sceneName, TPayload_2 isNewGame);
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}