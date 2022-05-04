using System.Collections.Generic;

namespace AI.FSM.Connections
{
    public class CombinedConnection<T> : StateConnection<T>
    {
        private List<StateConnection<T>> _connectedStates;

        public CombinedConnection(State<T> connectedState) : base(connectedState)
        {
            _connectedStates = new List<StateConnection<T>>();
        }

        public void AddConnection(StateConnection<T> connection)
        {
            _connectedStates.Add(connection);
        }
        
        public override bool IsConditionFulFilled()
        {
            bool condition = true;
            foreach (StateConnection<T> state in _connectedStates)
            {
                condition = state.IsConditionFulFilled();
                if (!condition) break;
            }

            return condition;
        }
    }
}