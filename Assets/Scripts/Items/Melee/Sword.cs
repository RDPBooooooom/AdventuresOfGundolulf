using LivingEntities;
using Interfaces;
using Effects;

namespace Items.Melee
{
    public class Sword : MeleeItem
    {
        #region Fields

        private IMelee _melee;
        private Bleeding _bleeding = new Bleeding(20, 1);

        #endregion

        #region Constructor

        public Sword() : base()
        {
            Value = 10;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            if (!(equipOn.GetType() == typeof(IMelee))) return;

            _melee = (IMelee)equipOn;
            _melee.Melee.AddEffect(_bleeding);

            equipOn.Attack += 10;
            _inGameUI.UpdateAttackDisplay();
    }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _melee.Melee.RemoveEffect(_bleeding);

            unequipFrom.Attack -= 10;
            _inGameUI.UpdateAttackDisplay();
        }

        #endregion
    }
}
