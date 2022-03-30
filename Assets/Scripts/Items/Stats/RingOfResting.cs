using LivingEntities;

namespace Items.Stats
{
    public class RingOfResting : StatsItem
    {
        float oldSpeed;
        public RingOfResting() : base()
        {
            Value = 10;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            oldSpeed = player.Speed;
            player.MaxHealth += 50;
            player.Speed = 0;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            player.MaxHealth -= 50;
            player.Speed = oldSpeed;
        }
    }
}