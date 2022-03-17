using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeleeItem
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Value = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
