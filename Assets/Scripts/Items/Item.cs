using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour, IInteractable
{
    // Class that all items will inherit from
    #region Fields

    [SerializeField] private Sprite _uIImage;
    private int _value;

    #endregion

    #region Properties

    public int Value 
    {
        get => _value; 
        protected set => _value = value; 
    }

    public Sprite UIImage
    {
        get => _uIImage;
        protected set => _uIImage = value;
    }

    #endregion

    void PickUpItem()
    {
        
    }

    public void Interact()
    {
        PickUpItem();
    }
}
