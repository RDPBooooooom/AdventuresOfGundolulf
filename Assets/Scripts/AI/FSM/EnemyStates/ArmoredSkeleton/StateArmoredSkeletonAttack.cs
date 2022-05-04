using LivingEntities;

namespace AI.FSM.EnemyStates.ArmoredSkeleton
{
    public class StateArmoredSkeletonAttack : ComplexStateAttack<ArmoredSkeletonEntity>
    {
        public StateArmoredSkeletonAttack(ArmoredSkeletonEntity owner) : base(owner)
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