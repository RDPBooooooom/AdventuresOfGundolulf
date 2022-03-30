using LivingEntities;

namespace Items.Stats
{
    public class MagicMilk : StatsItem
    {
        public MagicMilk() : base()
        {
            Value = 5;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            player.Speed += 10;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            player.Speed -= 10;
        }
    }
}