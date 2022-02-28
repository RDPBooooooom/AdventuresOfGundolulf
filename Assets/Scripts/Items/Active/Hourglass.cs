using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : Item, IUsable
{
    // Start is called before the first frame update
    void Start()
    {
        category = Category.Active;
        value = 15;
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
