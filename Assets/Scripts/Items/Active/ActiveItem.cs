using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItem : Item
{
    #region Properties

    public float Cooldown { get; protected set; }
    public bool IsReady { get; set; }

    #endregion

    #region Cooldown

    protected void StartCooldown()
    {
        //StartCoroutine(CooldownTimer());
    }

    protected IEnumerator CooldownTimer()
    {
        IsReady = false;

        yield return new WaitForSeconds(Cooldown);

        IsReady = true;
    }

    #endregion
}
