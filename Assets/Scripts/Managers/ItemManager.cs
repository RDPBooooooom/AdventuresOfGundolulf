using System;
using System.Collections.Generic;
using Items;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ItemManager : MonoBehaviour
    {
        #region Fields

        private ItemFactory _itemFactory;
        private List<Type> _availableItemTypes;
        private List<Type> _usedItemTypes;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _itemFactory = new ItemFactory();
            _availableItemTypes = _itemFactory.AllItemTypes;
            _usedItemTypes = new List<Type>();
        }

        #endregion

        #region Item Methods

        public void ItemEquipped(Item item)
        {
            Type type = item.GetType();
            _usedItemTypes.Add(type);
            _availableItemTypes.Remove(type);
        }

        public void ItemSold(Item item)
        {
            Type type = item.GetType();
            _availableItemTypes.Remove(type);
            _usedItemTypes.Remove(type);
        }

        public Item GetRandomItem()
        {
            Type type = ListUtils.GetRandomElement(_availableItemTypes);
            return _itemFactory.CreateInstanceOfItem(type);
        }

        public List<Item> GetRandomItems(int amount)
        {
            List<Type> randomItems = ListUtils.GetRandomElements(_availableItemTypes, amount);
            return _itemFactory.CreateInstanceOfItems(randomItems);
        }

        public Item GetSpecificItem(Type itemType)
        {
            return _itemFactory.CreateInstanceOfItem(itemType);
        }

        public DroppedItem GetRandomDroppedItem()
        {
            Item item = GetRandomItem();

            GameObject prefab = Resources.Load<GameObject>("Prefabs/Items/SpawnableItems/" + item.GetType().Name);

            if (prefab == null) return null;

            GameObject go = Instantiate(prefab);
            DroppedItem droppedItem = go.GetComponent<DroppedItem>();
            droppedItem.Item = item;

            return droppedItem;
        }

        #endregion
    }
}