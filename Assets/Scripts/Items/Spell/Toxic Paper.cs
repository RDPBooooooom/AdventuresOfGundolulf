using LivingEntities;
using Interfaces;
using Effects;

namespace Items.Spell
{
    public class ToxicPaper : SpellItem
    {
        ISpellcaster _spellcaster;
        Poison poison = new Poison(30, 5);

         public ToxicPaper()
        {
            Value = 15;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            // Add effects
            if (!(equipOn.GetType() == typeof(ISpellcaster))) return;

            _spellcaster = (ISpellcaster)equipOn;
            _spellcaster.SpellCast.AddEffect(poison);
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _spellcaster.SpellCast.RemoveEffect(poison);
        }
    }
}
