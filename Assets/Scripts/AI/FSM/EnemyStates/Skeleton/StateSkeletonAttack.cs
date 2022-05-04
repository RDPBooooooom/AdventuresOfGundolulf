using LivingEntities;

namespace AI.FSM.EnemyStates.Skeleton
{
    public class StateSkeletonAttack : ComplexStateAttack<SkeletonEntity>
    {
        public StateSkeletonAttack(SkeletonEntity owner) : base(owner)
        {
            owner.Melee.OnFinish += OnFinish;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.Melee.Use();
        }

        private void OnFinish()
        {
            IsFinished = true;
        }
    }
}