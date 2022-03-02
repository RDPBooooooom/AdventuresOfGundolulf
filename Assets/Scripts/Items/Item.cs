using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    // Class that all items will inherit from
    #region Declaring Variables


    
    #endregion

    public int value { get; protected set; }

    void PickUpItem()
    {
        
    }

    public void Interact()
    {
        PickUpItem();
    }



}
