using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Bleeding : Effect
    {
        private float _timePassed;

        public Bleeding(float effectChance, float effectDuration) : base(effectChance, effectDuration)
        {
            DPS = 30;
        }

        public override void TryApplyEffect(LivingEntity target)
        {
            if ((LivingEntity.Immunities.ImmuneToBleeding & target.Immunity) == 0)
            {
                base.TryApplyEffect(target);
            }
        }

        protected override void ApplyEffect(LivingEntity target, float duration)
        {
            // Start coroutine
            target.DamageEntity(DPS);
        }
    }
}