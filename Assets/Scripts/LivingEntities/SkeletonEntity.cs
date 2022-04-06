namespace LivingEntities
{
    public class SkeletonEntity : EnemyEntity
    {
        protected override void Awake()
        {
            base.Awake();
            Immunity |= Immunities.ImmuneToBleeding;
        }
    }
}