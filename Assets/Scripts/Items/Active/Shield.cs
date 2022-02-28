using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item, IUsable
{
    // Start is called before the first frame update
    void Start()
    {
        category = Category.Active;
        value = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }
}
