using LivingEntities;

namespace Items.Spell
{
    public class ReverseEye : SpellItem
    {

        public ReverseEye() : base()
        {
            Value = 15;
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
