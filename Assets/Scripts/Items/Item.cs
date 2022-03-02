using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    // Class that all items will inherit from
    #region Declaring Variables

    public int Value { get; protected set; }
    public Sprite uiImage;
    
    #endregion

    void PickUpItem()
    {
        
    }

    public void Interact()
    {
        PickUpItem();
    }
}
