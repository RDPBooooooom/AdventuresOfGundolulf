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
            equipOn.Speed += 10;
            _inGameUI.UpdateSpeedDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            unequipFrom.Speed -= 10;
            _inGameUI.UpdateSpeedDisplay();
        }
    }
}