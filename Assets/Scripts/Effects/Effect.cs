using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LivingEntities;
using Utils;

namespace Effects
{
    public class Effect
    {
        private float _dps;
        protected Timer _tickRate;

        public bool IsReady { get; private set; } = true;

        public float EffectChance { get; set; }
        public float EffectDuration { get; set; }

        public float DPS 
        { 
            get => _dps;
            set => _dps = value; 
        }

        public Effect(float effectChance, float effectDuration)
        {
            EffectChance = effectChance;
            EffectDuration = effectDuration;

            _tickRate = new Timer(MonoBehaviourDummy.Dummy, 1);
        }

        public virtual void TryApplyEffect(LivingEntity target)
        {
            if (CalculateChance(EffectChance))
            {
                MonoBehaviourDummy.Dummy.StartCoroutine(EffectTick(target));
            }
        }

        protected virtual void ApplyEffect(LivingEntity target)
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

        protected IEnumerator EffectTick(LivingEntity target)
        {
            float startTime = Time.time;
            while (Time.time - startTime < EffectDuration)
            {
                if (!_tickRate.IsReady) continue;

                ApplyEffect(target);

                _tickRate.Start();
            }
        }
    }
}