using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region Fields

    private string _itemsPath = "/Items";

    #endregion

    #region Properties

    public static ItemManager Instance { get; private set; }
    public List<Item> AllItems { get; private set; }
    public List<Item> NotEquippedItems { get; set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AllItems = new List<Item>((IEnumerable<Item>)Resources.LoadAll(_itemsPath));
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
        List<Item> randomItems = new List<Item>();

        if (NotEquippedItems.Count > amount)
        {
            while (randomItems.Count < amount)
            {
                int randomItem = Random.Range(0, NotEquippedItems.Count - 1);

                if (!randomItems.Contains(NotEquippedItems[randomItem]))
                {
                    randomItems.Add(NotEquippedItems[randomItem]);
                }
            }

            return randomItems;
        }
        else
        {
            return NotEquippedItems;
        }
    }

    #endregion
}
