using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    #region Fields

    [SerializeField] private Sprite _uIImage;
    [SerializeField] private Item _item;

    #endregion

    #region Properties

    public Item Item { get; set; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {

    }
}

