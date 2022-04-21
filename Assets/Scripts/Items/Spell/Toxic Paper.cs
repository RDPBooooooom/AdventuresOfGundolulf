using LivingEntities;
using Interfaces;
using Effects;

namespace Items.Spell
{
    public class ToxicPaper : SpellItem
    {
        #region Fields

        private ISpellcaster _spellcaster;
        private Poison _poison = new Poison(30, 5);

        #endregion

        #region Constructor

        public ToxicPaper()
        {
            Value = 15;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            if (!(equipOn.GetType() == typeof(ISpellcaster))) return;

            _spellcaster = (ISpellcaster)equipOn;
            _spellcaster.SpellCast.AddEffect(_poison);
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _spellcaster.SpellCast.RemoveEffect(_poison);
        }

        #endregion
    }
}
