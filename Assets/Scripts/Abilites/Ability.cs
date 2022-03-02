using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    #region Fields

    protected LivingEntity _owner;

    #endregion

    #region Properties

    public float Cooldown { get; protected set; }
    public bool IsReady { get; set; }

    #endregion

    public abstract void Use();

    public Ability(LivingEntity owner)
    {
        _owner = owner;
    }

    #region Cooldown

    protected void StartCooldown()
    {
        _owner.StartCoroutine(CooldownTimer());
    }

    protected IEnumerator CooldownTimer()
    {
        IsReady = false;

        yield return new WaitForSeconds(Cooldown);

        IsReady = true;
    }

    #endregion
}
