namespace AI.FSM
{
    public abstract class StateNestedFsm<T> : State<T>
    {
        protected State<T> _startingGlobalState;

        protected State<T> _startingState;

        protected StateNestedFsm(T owner, State<T> startingState) : base(owner)
        {
            _startingState = startingState;
            NestedStateMachine = new StateMachine<T>(owner, startingState);
        }

        protected StateNestedFsm(T owner, State<T> startingState, State<T> startingGlobalState) : base(owner)
        {
            _startingState = startingState;
            _startingGlobalState = startingGlobalState;
            NestedStateMachine = new StateMachine<T>(owner, startingState, startingGlobalState);
        }

        public StateMachine<T> NestedStateMachine { get; protected set; }

        public override void InitState(StateMachine<T> stateMachine)
        {
            base.InitState(stateMachine);
        }

        public override void OnEnter()
        {
            NestedStateMachine.ForceStart(_startingState, _startingGlobalState);
        }

        public override void OnUpdate()
        {
            NestedStateMachine.OnUpdate();
        }

        public override void OnExit()
        {
            NestedStateMachine.ForceExit();
        }
    }
}