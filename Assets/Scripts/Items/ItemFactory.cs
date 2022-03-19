using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Items
{
    public class ItemFactory
    {

        public List<Type> AllItemTypes { get; private set; }

        public ItemFactory()
        {
            AllItemTypes = ReflectiveEnumerator.GetAllItemTypes<Item>().ToList();
        }

        public Item CreateInstanceOfItem(Type type)
        {
            return (Item)Activator.CreateInstance(type);
        }

        public List<Item> CreateInstanceOfItems(List<Type> types)
        {
            List<Item> items = new List<Item>();
            types.ForEach(type => items.Add(CreateInstanceOfItem(type)));
            return items;
        }
    }
}