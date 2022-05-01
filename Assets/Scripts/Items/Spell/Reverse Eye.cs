using LivingEntities;

namespace Items.Spell
{
    public class ReverseEye : SpellItem
    {
        #region Constructor

        public ReverseEye() : base()
        {
            Value = 15;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            _player.SpellCastAttackPoint.Rotate(_player.transform.up, 180);
            _player.Intelligence += 20;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _player.SpellCastAttackPoint.Rotate(_player.transform.position, -180);
            _player.Intelligence -= 20;
        }

        #endregion
    }
}
