using LivingEntities;

namespace AI.FSM.EnemyStates.OrcShaman
{
    public partial class StateOrcShamanDeath : ComplexState<OrcShamanEntity>
    {
        public StateOrcShamanDeath(OrcShamanEntity owner) : base(owner)
        {
        }
    }
}