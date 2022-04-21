using LivingEntities;

namespace Items.Stats
{
    public class RingOfResting : StatsItem
    {
        #region Fields

        private float _oldSpeed;

        #endregion

        #region Constructor

        public RingOfResting() : base()
        {
            Value = 10;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            _oldSpeed = _player.Speed;
            equipOn.MaxHealth += 50;
            equipOn.HealEntity(50);
            _inGameUI.UpdateHealthbar();

            equipOn.Speed = 0.25f; //equipOn.Speed = 0;
            _inGameUI.UpdateSpeedDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.MaxHealth -= 50;

            if (unequipFrom.Health > unequipFrom.MaxHealth)
            {
                unequipFrom.Health = unequipFrom.MaxHealth;
            }
            _inGameUI.UpdateHealthbar();

            unequipFrom.Speed = _oldSpeed;
            _inGameUI.UpdateSpeedDisplay();
        }

        #endregion
    }
}