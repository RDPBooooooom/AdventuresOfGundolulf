using LivingEntities;

namespace Items.Stats
{
    public class MagicMilk : StatsItem
    {
        #region Constructor

        public MagicMilk() : base()
        {
            Value = 5;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            equipOn.Speed += 10;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.Speed -= 10;
        }

        #endregion
    }
}