using LivingEntities;

namespace Items.Melee
{
    public class Staff : MeleeItem
    {
        #region Constructor

        public Staff() : base()
        {
            Value = 5;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            equipOn.Attack -= 5;
            // Add KnockBack
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.Attack += 5;
        }

        #endregion
    }
}
