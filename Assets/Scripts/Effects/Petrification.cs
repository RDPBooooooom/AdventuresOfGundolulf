using LivingEntities;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Petrification : Effect
    {
        #region Constructor

        public Petrification(float effectChance, float effectDuration) : base(effectChance, effectDuration + 0.5f, effectDuration)
        {
            DPS = 0;
        }

        #endregion

        #region Effect

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

        #endregion
    }
}