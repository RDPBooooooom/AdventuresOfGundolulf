using LivingEntities;

namespace Items.Melee
{
    public class Pickaxe : MeleeItem
    {
        #region Fields

        LivingStatueEntity _livingStatueEntity;

        #endregion

        #region Constructor

        public Pickaxe() : base()
        {
            Value = 10;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            _livingStatueEntity.Immunity ^= LivingEntity.Immunities.ImmuneToMelee;

            /*
            if ()
            {
                _player.Attack *= 2;
            }*/

            equipOn.Attack += 10;
            //Add effect
            _inGameUI.UpdateAttackDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _livingStatueEntity.Immunity |= LivingEntity.Immunities.ImmuneToMelee;
            unequipFrom.Attack -= 10;
            _inGameUI.UpdateAttackDisplay();
        }

        #endregion
    }
}
