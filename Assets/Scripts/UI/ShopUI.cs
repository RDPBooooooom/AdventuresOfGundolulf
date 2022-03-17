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

namespace UserInterface
{
    public class ShopUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image[] _images;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;
        
        private Player _player;
        private List<Sprite> _itemSprites;
        private List<string> _itemValues;
        private Item _selectedItem;

        #endregion

        #region Properties

        public ShopGerald ShopGerald { get; set; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _player = GameManager.Instance.Player;
            //_images = GetComponentsInChildren<Image>().Where(go => go.gameObject != gameObject).ToArray();

            _itemSprites = new List<Sprite>();
            _itemValues = new List<string>();

            GeneratePreviews();
            DisplayItems();
        }

        private void Update()
        {
            if (_selectedItem == null)
            {
                _buyButton.interactable = false;
                _sellButton.interactable = false;
            }

            /*            
            if (_itemSprites.Count != ShopGerald.Assortment.Count)
            {
                GeneratePreviews();
                DisplayItems();
            }*/
            
        }
        #endregion

        #region UI Methods

        
        public void Buy()
        {
            ShopGerald.SellToPlayer(_selectedItem);
            _selectedItem = null;
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
            _selectedItem = ShopGerald.Assortment[index];

            _buyButton.interactable = true;
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
            _itemSprites.Clear();
            _itemValues.Clear();

            foreach (Item item in ShopGerald.Assortment)
            {
                Sprite preview = item.UIImage;
                _itemSprites.Add(preview);

                string value = item.Value.ToString();
                _itemValues.Add(value);
            }
        }

        public void DisplayItems()
        {
            for (int i = 0; i < _itemSprites.Count; i++)
            {
                _images[i].GetComponent<Image>().sprite = _itemSprites[i];
                _images[i].GetComponentInChildren<Text>().text = _itemValues[i];
            }
        }

        #endregion
    }
}



