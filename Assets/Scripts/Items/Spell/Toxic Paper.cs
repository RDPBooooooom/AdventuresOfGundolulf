using LivingEntities;

namespace Items.Spell
{
    public class ToxicPaper : SpellItem
    {
        public ToxicPaper()
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
