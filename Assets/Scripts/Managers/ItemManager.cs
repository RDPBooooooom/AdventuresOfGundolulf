using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System.Linq;

public class ItemManager : MonoBehaviour
{
    #region Fields

    private string _itemsPath = "Items";

    #endregion

    #region Properties

    public List<Item> AllItems { get; private set; }
    public List<Item> NotEquippedItems { get; set; }

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        AllItems = new List<Item>();
        AllItems = Resources.LoadAll(_itemsPath, typeof(Item)).Cast<Item>().ToList();

        NotEquippedItems = AllItems;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Methods

    public void ItemEquipped(Item item)
    {
        NotEquippedItems.Remove(item);
    }

    public void ItemSold(Item item)
    {
        NotEquippedItems.Add(item);
    }

    public List<Item> RandomItem(int amount)
    {
        if (NotEquippedItems.Count > amount)
        {
            List<Item> randomItems = ListUtils.GetRandomElements(NotEquippedItems, amount);

            return randomItems;
        }
        else
        {
            return NotEquippedItems;
        }
    }
    
    #endregion
}
