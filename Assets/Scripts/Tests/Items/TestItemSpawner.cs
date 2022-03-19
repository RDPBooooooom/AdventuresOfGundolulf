using System;
using System.Collections.Generic;
using Items;
using Managers;
using UnityEngine;

namespace Tests.Items
{
    public class TestItemSpawner : MonoBehaviour
    {
        private Vector3 _currentPos = new Vector3(0, 1, 0);

        private void Start()
        {
            ItemFactory itemFactory = new ItemFactory();
            List<Type> allItemTypes = itemFactory.AllItemTypes;
            List<Item> items = itemFactory.CreateInstanceOfItems(allItemTypes);

            foreach (Item item in items)
            {
                GameObject prefab = Resources.Load<GameObject>("Prefabs/Items/" + item.GetType().Name);

                if (prefab == null)
                {
                    Debug.LogError("Prefab for Item " + item.GetType().Name + " was not found!");
                    continue;
                }

                GameObject go = Instantiate(prefab);
                go.transform.position = _currentPos;

                if (!go.TryGetComponent(out DroppedItem droppedItem))
                {
                    Debug.LogError("Prefab for Item " + item.GetType().Name + " doesn't have a DroppedItem Component!");
                    Destroy(go);
                    continue;
                }
                
                droppedItem.Item = item;

                _currentPos += new Vector3(3, 0, 0);
            }
        }
    }
}