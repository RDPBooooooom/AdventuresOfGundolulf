using Interfaces;

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

            if (!(equipOn.GetType() == typeof(ISpellcaster))) return;

            ISpellcaster spellcaster = (ISpellcaster) equipOn;
            equipOn.Range -= 175;
            inGameUI.UpdateRangeDisplay();
        }


        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            unequipFrom.Range += 175;
            inGameUI.UpdateRangeDisplay();
        }
    }
}
