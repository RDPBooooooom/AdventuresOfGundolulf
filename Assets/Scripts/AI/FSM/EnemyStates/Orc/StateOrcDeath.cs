using LivingEntities;

namespace AI.FSM.EnemyStates.Orc
{
    public class StateOrcDeath : ComplexState<OrcEntity>
    {
        public StateOrcDeath(OrcEntity owner) : base(owner)
        {
        }
    }
}