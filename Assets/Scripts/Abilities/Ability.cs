using System.Collections.Generic;
using Effects;
using LivingEntities;
using Utils;

namespace Abilities
{
    public abstract class Ability
    {
        #region Fields

        private float _cooldown;

        protected LivingEntity _owner;
        protected Timer _cooldownTimer;
        protected List<Effect> _effects;

        #endregion

        #region Properties

        public float Cooldown
        {
            get => _cooldown;
            protected set
            {
                _cooldown = value;
                if (_cooldownTimer != null) _cooldownTimer.Time = value;
            }
        }

        public bool IsReady => _cooldownTimer.IsReady;

        #endregion

        #region Delegates

        public delegate void AbilityFinishDelegate();

        #endregion

        #region Events

        public event AbilityFinishDelegate OnFinish;

        #endregion

        public abstract void Use();

        #region Constructor

        protected Ability(LivingEntity owner)
        {
            _owner = owner;
            _cooldownTimer = new Timer(_owner, Cooldown);
            _effects = new List<Effect>();
        }

        #endregion

        #region Cooldown

        protected void StartCooldown()
        {
            _cooldownTimer.Start();
        }

        #endregion

        protected void OnAbilityFinshed()
        {
            OnFinish?.Invoke();
        }

        #region Effects

        public void AddEffect(Effect effect)
        {
            _effects.Add(effect);
        }

        public void AddEffects(List<Effect> effectsToAdd)
        {
            _effects.AddRange(effectsToAdd);
        }

        public void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect);
        }

        #endregion
    }
}