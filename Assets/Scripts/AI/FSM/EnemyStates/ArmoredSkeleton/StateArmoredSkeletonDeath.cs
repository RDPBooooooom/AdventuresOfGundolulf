using LivingEntities;

namespace AI.FSM.EnemyStates.ArmoredSkeleton
{
    public class StateArmoredSkeletonDeath : ComplexState<ArmoredSkeletonEntity>
    {
        public StateArmoredSkeletonDeath(ArmoredSkeletonEntity owner) : base(owner)
        {
        }
    }
}