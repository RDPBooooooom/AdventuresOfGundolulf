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
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
        }
    }
}
