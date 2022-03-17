using Scrolls;
using Scrolls.StandardScrolls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CastScrollUI : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Button _castButton;
        [SerializeField] private Button[] _selectButtons;

        private Scroll _selectedScroll;

        #endregion

        #region Properties

        public List<Scroll> DrawnScrolls { get; set; }

        #endregion

        #region Unity Methods

        private void Update()
        {
            if (_selectedScroll == null)
            {
                _castButton.interactable = false;
            }
        }

        #endregion

        #region UI Methods

        public void SelectScroll(int index)
        {
            _selectedScroll = DrawnScrolls[index];

            _castButton.interactable = true;
        }

        public void CastScroll()
        {
            // Use Scroll
            _selectedScroll.Activate();

            DrawnScrolls.Remove(_selectedScroll);
            _selectedScroll = null;


            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            for (int i = 0; i < _selectButtons.Length; i++)
            {
                _selectButtons[i].GetComponentsInChildren<Text>()[0].text = DrawnScrolls[i].GetType().Name;
            }
        }

        #endregion
    }
}