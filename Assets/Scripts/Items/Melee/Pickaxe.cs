using Interfaces;
using LivingEntities;

namespace Items.Melee
{
    public class Pickaxe : MeleeItem
    {
        #region Fields

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

            if ((equipOn.GetType() == typeof(IMelee))) 
            { 
                IMelee iMelee = (IMelee)equipOn;
                iMelee.Melee.OnDamage += DamageEntity;
            }

            equipOn.Attack += 10;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.Attack -= 10;
        }

        private void DamageEntity(LivingEntity target, int damage)
        {
            target.DamageEntity(damage);
        }

        #endregion
    }
}
