namespace AI.FSM
{
    public abstract class StateConnection<T>
    {
        public StateConnection(State<T> connectedState)
        {
            ConnectedState = connectedState;
        }

        public State<T> ConnectedState { get; }

        public virtual void OnEnter()
        {
        }

        public abstract bool IsConditionFulFilled();
    }
}