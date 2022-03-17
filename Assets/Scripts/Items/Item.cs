using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using PlayerScripts;

public class Item
{
    // Class that all items will inherit from
    #region Fields

    private Sprite _uIImage;
    private int _value;
    private Player _player;

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

    #region Unity Methods

    public Item()
    {

    }

    protected virtual void Start()
    {
        _player = GameManager.Instance.Player;
    }

    #endregion

    void PickUpItem()
    {
        if (!_player.EquippedItems.Contains(this))
        {
            _player.EquippedItems.Add(this);
        }
    }


}

