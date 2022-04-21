using LivingEntities;
using Interfaces;
using Effects;

namespace Items.Melee
{
    public class Sword : MeleeItem
    {
        private IMelee _melee;
        private Bleeding _bleeding = new Bleeding(20, 1);

        public Sword() : base()
        {
            Value = 10;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            // Add effect
            if (!(equipOn.GetType() == typeof(IMelee))) return;

            _melee = (IMelee)equipOn;
            _melee.Melee.AddEffect(_bleeding);

            equipOn.Attack += 10;
            inGameUI.UpdateAttackDisplay();
    }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            // Remove effect
            _melee.Melee.RemoveEffect(_bleeding);

            unequipFrom.Attack -= 10;
            inGameUI.UpdateAttackDisplay();
        }
    }
}
