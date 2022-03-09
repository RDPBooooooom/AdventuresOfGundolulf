using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UserInterface
{
    public class ShopUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private RawImage[] _images;

        private ShopGerald _shopGerald;
        private List<Texture2D> _itemTextures;
        private Item _selectedItem;

        //private Dictionary<Item, Texture2D> _previews;

        #endregion

        #region Properties

        public static ShopUI Instance { get; private set; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _shopGerald = FindObjectOfType<ShopGerald>();
        }

        private void Update()
        {
            if (_selectedItem == null)
            {
                _buyButton.interactable = false;
                _sellButton.interactable = false;
            }

            if (_itemTextures.Count != _shopGerald.Assortment.Count)
            {
                GeneratePreviews();
                DisplayItems();
            }
        }
        #endregion

        #region UI Methods

        
        public void Buy()
        {
            _shopGerald.SellToPlayer(_selectedItem);
            _selectedItem = null;
        }

        public void Sell()
        {
            _shopGerald.BuyFromPlayer(_selectedItem);
        }

        public void AssortmentUI()
        {
            _shopPanel.SetActive(true);
        }

        public void Back()
        {
            _shopPanel.SetActive(false);
        }

        public void SelectItemToBuy(int index)
        {
            _selectedItem = _shopGerald.Assortment[index];

            _buyButton.interactable = true;
        }

        public void SelectItemToSell()
        {
            _sellButton.interactable = true;
        }

        #endregion

        #region Helper Methods

        private void GeneratePreviews()
        {
            foreach (Item item in _shopGerald.Assortment)
            {
                Texture2D preview = AssetPreview.GetAssetPreview(item.gameObject);
                _itemTextures.Add(preview);

                /*
                if (!_previews.ContainsKey(item))
                {
                    Texture2D preview = AssetPreview.GetAssetPreview(item.gameObject);

                    if (preview != null)
                    {
                        _previews.Add(item, preview);
                    }
                }*/
            }
        }

        public void DisplayItems()
        {
            for (int i = 0; i < _itemTextures.Count; i++)
            {
                _images[i].GetComponent<RawImage>().texture = _itemTextures[i];
            }
        }

        #endregion
    }
}



