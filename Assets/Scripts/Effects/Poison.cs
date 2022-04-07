using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Poison : Effect
    {
        public Poison(float effectChance, float effectDuration) : base(effectChance, effectDuration)
        {
            DPS = 10;
        }

        public override void TryApplyEffect(LivingEntity target)
        {
            if ((LivingEntity.Immunities.ImmuneToPoison & target.Immunity) == 0)
            {
                base.TryApplyEffect(target);
            }
        }

        protected override void ApplyEffect(LivingEntity target)
        {
            target.DamageEntity(DPS);
        }
    }
}