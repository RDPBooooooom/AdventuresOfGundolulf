using LivingEntities;

namespace AI.FSM.Connections
{
    public class StateConnectionOnDeath<T> : StateConnection<T> where T : LivingEntity
    {
        private T _owner;
        
        public StateConnectionOnDeath(State<T> connectedState, T owner) : base(connectedState)
        {
            _owner = owner;
        }

        public override bool IsConditionFulFilled()
        {
            return !_owner.IsAlive;
        }
    }
}