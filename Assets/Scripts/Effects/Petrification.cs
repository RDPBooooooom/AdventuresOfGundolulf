using LivingEntities;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Petrification : Effect
    {
        float _timePassed;

        public Petrification()
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

        protected override void ApplyEffect(LivingEntity target, float duration)
        {
            _timePassed += Time.deltaTime;

            if (_timePassed <= duration)
            {
                target.StopActions = true;
                // Make the character look gray
            }
            else
            {
                target.StopActions = false;
                // Reset character
            }
        }
    }
}