using LivingEntities;

namespace AI.FSM.EnemyStates.OrcShaman
{
    public class StateOrcShamanChase : ComplexState<OrcShamanEntity>
    {

        private LivingEntity _toChase;
        
        public StateOrcShamanChase(OrcShamanEntity owner, LivingEntity toChase) : base(owner)
        {
            _toChase = toChase;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.Target = _toChase;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            _owner.UpdateVelocity();
            _owner.MoveEntity();
        }

        public override void OnExit()
        {
            base.OnExit();
            
            _owner.ResetVelocity();
        }
    }
}