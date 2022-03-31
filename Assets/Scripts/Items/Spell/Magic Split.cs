using LivingEntities;

namespace Items.Spell
{
    public class MagicSplit : SpellItem
    {
        public MagicSplit() : base()
        {
            Value = 20;
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
