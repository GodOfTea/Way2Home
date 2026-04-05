using System;
using System.Collections.Generic;
using Infrastructure.Services.Factory;
using Infrastructure.Services.PersistentProgreses;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.Settings;

namespace Infrastructure.Services.State
{
    public class GameStateMachine : IStateSwitcher
    {
        private readonly Dictionary<Type, IExitableState> _states;
        
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressesService>(), 
                    services.Single<ISaveLoadService>()),
                [typeof(LoadMainMenuState)] = new LoadMainMenuState(this, sceneLoader, services.Single<IGameFactory>(), 
                    services.Single<IPersistentProgressesService>()),
                [typeof(MainMenuState)] = new MainMenuState(this),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, services.Single<IGameFactory>(), 
                    services.Single<IPersistentProgressesService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<IGameFactory>(), 
                    services.Single<IPersistentProgressesService>(), services.Single<ISettings>()),
                [typeof(GameExitState)] = new GameExitState(services.Single<ISaveLoadService>()),
            };
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        public void Enter<TState, TPayload_1, TPayload_2>(TPayload_1 payload_1, TPayload_2 payload_2) where TState : class, IPayloadedState<TPayload_1, TPayload_2>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload_1, payload_2);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}