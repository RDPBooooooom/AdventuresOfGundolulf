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
            equipOn.HealEntity(50);
            inGameUI.UpdateHealthbar();

            equipOn.Speed = 0.25f; //equipOn.Speed = 0;
            inGameUI.UpdateSpeedDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.MaxHealth -= 50;

            if (unequipFrom.Health > unequipFrom.MaxHealth)
            {
                unequipFrom.Health = unequipFrom.MaxHealth;
            }
            inGameUI.UpdateHealthbar();

            unequipFrom.Speed = oldSpeed;
            inGameUI.UpdateSpeedDisplay();
        }
    }
}