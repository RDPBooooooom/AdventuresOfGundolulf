using LivingEntities;

namespace AI.FSM.EnemyStates.Skeleton
{
    public class StateSkeletonChase : ComplexState<SkeletonEntity>
    {
        private LivingEntity _toChase;
        
        public StateSkeletonChase(SkeletonEntity owner, LivingEntity toChase) : base(owner)
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