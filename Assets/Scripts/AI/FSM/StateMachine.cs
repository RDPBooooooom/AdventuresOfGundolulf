namespace AI.FSM
{
    public class StateMachine<T>
    {
        private State<T> _currentGlobalState;
        private T _owner;

        public StateMachine(T owner, State<T> startingState)
        {
            _owner = owner;
            CurrentState = startingState;
        }

        public StateMachine(T owner, State<T> startingState, State<T> startingGlobalState)
        {
            _owner = owner;
            CurrentState = startingState;
            _currentGlobalState = startingGlobalState;
        }

        public State<T> CurrentState { get; private set; }

        public void StartStateMachine()
        {
            CurrentState.OnEnter();
        }

        public void ForceStart(State<T> state, State<T> globalState)
        {
            CurrentState = state;
            _currentGlobalState = globalState;

            CurrentState.OnEnter();
            _currentGlobalState?.OnEnter();
        }

        public void ForceExit()
        {
            CurrentState.OnExit();
            _currentGlobalState?.OnExit();
        }

        public void OnUpdate()
        {
            _currentGlobalState?.OnUpdate();


            CurrentState.OnUpdate();
        }

        public void ChangeState(State<T> newState)
        {
            CurrentState.OnExit();

            CurrentState = newState;
            CurrentState.OnEnter();
        }

        public void ChangeGlobalState(State<T> newGlobalState)
        {
            _currentGlobalState?.OnExit();

            _currentGlobalState = newGlobalState;

            _currentGlobalState?.OnEnter();
        }
    }
}