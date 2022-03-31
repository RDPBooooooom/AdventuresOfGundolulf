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
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
        }
    }
}
