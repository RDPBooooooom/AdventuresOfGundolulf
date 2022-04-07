using LivingEntities;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Petrification : Effect
    {
        public Petrification(float effectChance, float effectDuration) : base(effectChance, effectDuration + 0.5f, effectDuration)
        {
            DPS = 0;
        }

        public override void TryApplyEffect(LivingEntity target)
        {
            if ((LivingEntity.Immunities.ImmuneToPetrification & target.Immunity) == 0)
            {
                base.TryApplyEffect(target);
            }
        }

        protected override void ApplyEffect(LivingEntity target)
        {
            target.StopActions = !target.StopActions;
        }
    }
}