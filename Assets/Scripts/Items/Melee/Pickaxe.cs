using LivingEntities;

namespace Items.Melee
{
    public class Pickaxe : MeleeItem
    {

        public Pickaxe() : base()
        {
            Value = 10;
        }
        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            equipOn.Attack += 10;
            //Add effect
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
