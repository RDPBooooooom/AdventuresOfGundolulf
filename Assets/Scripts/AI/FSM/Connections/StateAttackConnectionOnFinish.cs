namespace AI.FSM.Connections
{
    public class StateAttackConnectionOnFinish<T> : StateConnection<T>
    {

        private ComplexStateAttack<T> _stateToFinish;

        public StateAttackConnectionOnFinish(State<T> connectedState, ComplexStateAttack<T> stateToFinish) : base(connectedState)
        {
            _stateToFinish = stateToFinish;
        }

        public override bool IsConditionFulFilled()
        {
            return _stateToFinish.IsFinished;
        }
    }
}