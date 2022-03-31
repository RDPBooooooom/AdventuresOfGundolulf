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
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
        }
    }
}
