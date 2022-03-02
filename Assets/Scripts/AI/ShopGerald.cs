using System.Collections;
using Random = System.Random;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ShopGerald : MonoBehaviour, IInteractable
{
    #region Declaring Variables

    int money = 250; // ? Depends on difficulty ?
    List<Item> equippedItems;
    int amountOfItems = 5;
    List<Item> items =  new List<Item>() { new AmulettofRegeneration(), new Hourglass(), new MagicMilk(), new MagicSplit(), new Pickaxe(), new ReverseEye(), new RingofResting(), new Shield(), new Staff(), new Swoop(), new Sword(), new ToxicPaper(), new HealthPotion()}; // List with All items
    int valueLossFactor = 2;

    #endregion

    void Sell(Item item)
    {
        money += item.Value;
        equippedItems.Remove(item);
        //TO DO: Add to player
    }

    void Buy(Item item)
    {
        if (money >= Mathf.RoundToInt(item.Value / valueLossFactor)) // TO DO: Abrunden
        {
            money -= Mathf.RoundToInt(item.Value / valueLossFactor);
            equippedItems.Add(item);
            //TO DO: Remove from Player
        }
        else
            Debug.LogError("Merchant does not have enough Money !");
    }

    List<Item> GetRandomInventory()
    {
        Random random = new Random();
        List<Item> itemsSale = new List<Item>();
        while(itemsSale.Count -1 < amountOfItems)
        {
            int randomItem = random.Next(0, items.Count - 1);
            if (!itemsSale.Contains(items[randomItem]))
                itemsSale.Add(items[randomItem]);

        }
        return itemsSale;
    }

    void ShowAssortment()
    {
        // Gets executed if interacting
        GetRandomInventory();
    }

    public void Interact()
    {
        ShowAssortment();
    }
}