using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using PlayerScripts;

public class ShopGerald : MonoBehaviour, IInteractable
{
    #region Declaring Variables

    private Player _player;

    private int _merchantMoney = 250; // Depends on difficulty?
    private int _amountOfItems = 5;
    private int _valueLossFactor = 2;

    private List<Item> _assortment;

    #endregion
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    void Sell(Item item)
    {
        _merchantMoney += item.Value;
        _assortment.Remove(item);

        _player.EquippedItems.Add(item);

        ItemManager.Instance.ItemEquipped(item);
    }

    void Buy(Item item)
    {
        int price = Mathf.FloorToInt(item.Value / _valueLossFactor);

        if (_merchantMoney >= price)
        {
            _merchantMoney -= price;
            _assortment.Add(item);

            _player.Gold += price;
            _player.EquippedItems.Remove(item);

            ItemManager.Instance.ItemSold(item);
        }
        else
        {
            // ToDo: Display Text, that the merchant doesn't have enough money
            Debug.LogError("Merchant does not have enough Money!");
        }
    }

    void ShowAssortment()
    {
        // Gets executed if interacting
        ItemManager.Instance.RandomItem(_amountOfItems);

        // ToDo: DisplayAssortment + Implement Sell/Buy
    }

    public void Interact()
    {
        ShowAssortment();
    }
}