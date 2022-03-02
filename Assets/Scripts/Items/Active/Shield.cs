using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ActiveItem, IUsable
{
    // Start is called before the first frame update
    void Start()
    {
        Value = 20;
        Cooldown = 30;
        IsReady = true; // Be careful not to be able to abuse -> switch active item and use instantly
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Effect()
    {
        // Do effect

        StartCooldown();
    }

    public void Use()
    {
        Effect();
    }
}
