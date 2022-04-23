using System.Collections.Generic;
using System.Linq;

namespace AI.FSM
{
    public abstract class ComplexState<T> : State<T>
    {
        protected List<StateConnection<T>> Connections;

        protected ComplexState(T owner) : base(owner)
        {
            Connections = new List<StateConnection<T>>();
        }

        public void AddConnection(StateConnection<T> connection)
        {
            Connections.Add(connection);
        }

        public override void InitState(StateMachine<T> stateMachine)
        {
            base.InitState(stateMachine);
        }

        public override void OnEnter()
        {
            base.OnEnter();
            foreach (var c in Connections) c.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            foreach (var c in Connections.Where(c => c.IsConditionFulFilled()))
            {
                _stateMachine.ChangeState(c.ConnectedState);
                break;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}