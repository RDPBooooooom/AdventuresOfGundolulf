using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Bleeding : Effect
    {
        private float _timePassed;
        public Bleeding()
        {
            DPS = 30;
        }

        public override void TryApplyEffect(LivingEntity target, float effectChance, float effectDuration)
        {
            if ((LivingEntity.Immunities.ImmuneToBleeding & target.Immunity) == 0)
            {
                base.TryApplyEffect(target, effectChance, effectDuration);
            }
        }

        protected override void ApplyEffect(LivingEntity target, float duration)
        {
            do
            {
                _timePassed += Time.deltaTime;

                if (IsReady)
                {
                    target.DamageEntity(DPS);
                    // Show Particles?
                    target.StartCoroutine(TickRate());
                }
            } while (_timePassed <= duration);
        }
    }
}