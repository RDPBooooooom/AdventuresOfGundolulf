using Abilities;

namespace AI.FSM.Connections
{
    public class StateConnectionAbilityReady<T> : StateConnection<T>
    {
        private Ability _ability;
        
        public StateConnectionAbilityReady(State<T> connectedState, Ability ability) : base(connectedState)
        {
            _ability = ability;
        }

        public override bool IsConditionFulFilled()
        {
            return _ability.IsReady;
        }
    }
}