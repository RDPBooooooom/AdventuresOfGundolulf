using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using Utils;

public abstract class Ability
{
    #region Fields

    protected LivingEntity _owner;
    protected Timer _cooldownTimer;

    protected List<Effect> _effects;

    private float _cooldown;

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

    public abstract void Use();

    protected Ability(LivingEntity owner)
    {
        _owner = owner;
        _cooldownTimer = new Timer(_owner, Cooldown);
        _effects = new List<Effect>();
    }

    #region Cooldown

    protected void StartCooldown()
    {
        _cooldownTimer.Start();
    }

    #endregion

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