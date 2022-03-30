using Managers;
using PlayerScripts;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Items;

namespace UserInterface
{
    public class ShopUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image[] _images;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;
        
        private Player _player;
        private List<Item> _items;
        private Item _selectedItem;
        private int _selectedIndex;

        #endregion

        #region Properties

        public ShopGerald ShopGerald { get; set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            _items = new List<Item>();
        }

        private void Start()
        {
            _player = GameManager.Instance.Player;

            GeneratePreviews();
        }

        private void Update()
        {
            if (_selectedItem != null) return;
            
            _buyButton.interactable = false;
            _sellButton.interactable = false;

        }
        #endregion

        #region UI Methods

        
        public void Buy()
        {
            ShopGerald.SellToPlayer(_selectedItem);
            Destroy(_images[_selectedIndex].gameObject);
                
            _selectedItem = null;
            _selectedIndex = -1;
        }

        public void Sell()
        {
            ShopGerald.BuyFromPlayer(_selectedItem);
        }

        public void Back()
        {
            Destroy(gameObject);
        }

        public void SelectItemToBuy(int index)
        {
            _selectedItem = _items[index];
            _selectedIndex = index;

            if (!ShopGerald.Assortment[_selectedItem])
            {
                _selectedIndex = -1;
                _selectedItem = null;
                return;
            }

            if(_selectedItem.Value <= _player.Gold) _buyButton.interactable = true;
        }

        public void SelectItemToSell(int index)
        {
            // ToDo: Assign an index to each item of the player
           
           // _selectedItem = _player.EquippedItems[index];

            _sellButton.interactable = true;
        }

        #endregion

        #region Helper Methods

        private void GeneratePreviews()
        {
            foreach (KeyValuePair<Item, bool> kvp in ShopGerald.Assortment)
            {
                if (!kvp.Value) continue;

                Item item = kvp.Key;
                _items.Add(item);
            }

            int i = 0;
            foreach (Item item in _items)
            {
                if (i > 4) break;
                
                _images[i].GetComponent<Image>().sprite = item.UIImage;
                _images[i].GetComponentInChildren<Text>().text = item.Value.ToString();

                i++;
            }

            foreach (Image image in _images)
            {
                if (image.GetComponentInChildren<Text>().text.Equals("Default"))
                {
                    Destroy(image.gameObject);
                }
            }
        }
        #endregion
    }
}



