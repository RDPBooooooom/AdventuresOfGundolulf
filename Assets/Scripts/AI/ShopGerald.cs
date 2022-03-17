using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using PlayerScripts;
using UserInterface;
using Managers;

public class ShopGerald : MonoBehaviour, IInteractable
{
    #region Fields

    [SerializeField] private ShopUI _shopPanelPrefab;

    private Player _player;
    private ShopUI _shopUI;

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
        _player = GameManager.Instance.Player;
    }

    public void SellToPlayer(Item item)
    {
        _merchantMoney += item.Value;
        Assortment.Remove(item);

        _player.EquippedItems.Add(item);

        GameManager.Instance.ItemManager.ItemEquipped(item);
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

            GameManager.Instance.ItemManager.ItemSold(item);
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
            Assortment = GameManager.Instance.ItemManager.RandomItem(_amountOfItems);
            Assortment.Add(Resources.Load<Item>(_potionPath));
        }

        if (_shopUI == null)
        {
            _shopUI = Instantiate(_shopPanelPrefab, GameManager.Instance.UIManager.MainCanvas.transform);
            _shopUI.ShopGerald = this;
        }
    }

    public void Interact()
    {
        ShowAssortment();
    }
}