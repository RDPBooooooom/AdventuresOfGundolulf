using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Poison : Effect
    {
        private float _timePassed;

        public Poison()
        {
            DPS = 10;
        }

        public override void TryApplyEffect(LivingEntity target, float effectChance, float effectDuration)
        {
            if ((LivingEntity.Immunities.ImmuneToPoison & target.Immunity) == 0)
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
                    // Show particles?
                    target.StartCoroutine(TickRate());
                }
            } while (_timePassed <= duration);
        }
    }
}