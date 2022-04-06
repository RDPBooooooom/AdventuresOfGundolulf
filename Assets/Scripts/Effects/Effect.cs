using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LivingEntities;

namespace Effects
{
    public class Effect
    {
        private float _dps;

        public bool IsReady { get; private set; } = true;

        public float DPS 
        { 
            get => _dps;
            set => _dps = value; 
        }

        public virtual void TryApplyEffect(LivingEntity target, float effectChance, float effectDuration)
        {
            if (CalculateChance(effectChance))
            {
                ApplyEffect(target, effectDuration);
            }
        }

        protected virtual void ApplyEffect(LivingEntity target, float duration)
        {
            
        }

        public bool CalculateChance(float chance)
        {
            float random = Random.Range(0, 100);

            if (random <= chance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected IEnumerator TickRate()
        {
            IsReady = false;

            yield return new WaitForSeconds(1);

            IsReady = true;
        }
    }
}