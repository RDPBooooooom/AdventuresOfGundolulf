using LivingEntities;

namespace Items.Melee
{
    public class Staff : MeleeItem
    {
        public Staff() : base()
        {
            Value = 5;
        }
        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            equipOn.Attack -= 5;
            // Add KnockBack
            inGameUI.UpdateAttackDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            unequipFrom.Attack += 5;
            inGameUI.UpdateAttackDisplay();
        }
    }
}
