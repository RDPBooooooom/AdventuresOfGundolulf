using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using PlayerScripts;
using UserInterface;

public class ShopGerald : MonoBehaviour, IInteractable
{
    #region Fields

    private Player _player;

    private int _merchantMoney = 250; // Depends on difficulty?
    private int _amountOfItems = 4;
    private int _valueLossFactor = 2;
    private string _potionPath = "HealthPotion";

    #endregion

    #region Properties

    public List<Item> Assortment { get; private set; }

    #endregion
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    public void SellToPlayer(Item item)
    {
        _merchantMoney += item.Value;
        Assortment.Remove(item);

        _player.EquippedItems.Add(item);

        ItemManager.Instance.ItemEquipped(item);
    }

    public void BuyFromPlayer(Item item)
    {
        int price = Mathf.FloorToInt(item.Value / _valueLossFactor);

        if (_merchantMoney >= price)
        {
            _merchantMoney -= price;
            Assortment.Add(item);

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
        if (Assortment == null)
        {
            Assortment = ItemManager.Instance.RandomItem(_amountOfItems);
            Assortment.Add((Item)Resources.Load(_potionPath));
        }

        ShopUI.Instance.AssortmentUI();
    }

    public void Interact()
    {
        ShowAssortment();
    }
}