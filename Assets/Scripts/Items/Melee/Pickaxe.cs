using LivingEntities;

namespace Items.Melee
{
    public class Pickaxe : MeleeItem
    {
        LivingStatueEntity livingStatueEntity;

        public Pickaxe() : base()
        {
            Value = 10;
        }
        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            // Add effect
            livingStatueEntity.Immunity ^= LivingEntity.Immunities.ImmuneToMelee;

            /*
            if ()
            {
                _player.Attack *= 2;
            }*/

            equipOn.Attack += 10;
            inGameUI.UpdateAttackDisplay();
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            // Remove effect
            livingStatueEntity.Immunity |= LivingEntity.Immunities.ImmuneToMelee;
            unequipFrom.Attack -= 10;
            inGameUI.UpdateAttackDisplay();
        }
    }
}
