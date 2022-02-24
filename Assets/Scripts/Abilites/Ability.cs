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

    public float cooldown { get; protected set; }
    public bool isReady { get; set; }

    #endregion

    public abstract void Use();

    public Ability(LivingEntity owner)
    {
        _owner = owner;
    }

    #region Cooldown

    protected void StartCooldown()
    {
        _owner.StartCoroutine(Cooldown());
    }

    protected IEnumerator Cooldown()
    {
        isReady = false;

        yield return new WaitForSeconds(cooldown);

        isReady = true;
    }

    #endregion
}
