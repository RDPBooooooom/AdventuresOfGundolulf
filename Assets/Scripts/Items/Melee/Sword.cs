using LivingEntities;

namespace Items.Melee
{
    public class Sword : MeleeItem
    {
        public Sword() : base()
        {
            Value = 10;
        }
        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            equipOn.Attack += 10;
            //Add 20% chance for bleeding
            inGameUI.UpdateAttackDisplay();
    }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            unequipFrom.Attack -= 10;
            inGameUI.UpdateAttackDisplay();
        }
    }
}
