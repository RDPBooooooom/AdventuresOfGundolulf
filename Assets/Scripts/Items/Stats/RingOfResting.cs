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
            oldSpeed = _player.Speed;
            equipOn.MaxHealth += 50;
            equipOn.Speed = 0;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            unequipFrom.MaxHealth -= 50;
            unequipFrom.Speed = oldSpeed;
        }
    }
}