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

            //Add effect
            _player.SpellCastAttackPoint.Rotate(_player.transform.position, 180);
            _player.Intelligence += 20;
            inGameUI.UpdateIntelligenceDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            _player.SpellCastAttackPoint.Rotate(_player.transform.position, -180);
            _player.Intelligence -= 20;
            inGameUI.UpdateIntelligenceDisplay();
        }

    }
}
