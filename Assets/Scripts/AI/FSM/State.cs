namespace AI.FSM
{
    public abstract class State<T>
    {
        protected T _owner;

        protected StateMachine<T> _stateMachine;

        protected State(T owner)
        {
            _owner = owner;
        }

        public virtual void InitState(StateMachine<T> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}