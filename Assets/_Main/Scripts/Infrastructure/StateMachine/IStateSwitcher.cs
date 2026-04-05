namespace Infrastructure.Services.State
{
    public interface IStateSwitcher : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        void Enter<TState, TPayload_1, TPayload_2>(TPayload_1 payload, TPayload_2 secondPayload) where TState : class, IPayloadedState<TPayload_1, TPayload_2>;
    }
}