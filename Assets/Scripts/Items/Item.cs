using LivingEntities;
using UnityEngine;
using PlayerScripts;
using Managers;
using UI;

namespace Items
{
    public class Item
    {
        // Class that all items will inherit from

        #region Fields

        private Sprite _uIImage;
        protected int _value;
        protected Player _player;
        protected InGameUI _inGameUI;

        #endregion

        #region Properties

        public int Value
        {
            get => _value;
            protected set => _value = value;
        }

        public Sprite UIImage
        {
            get => _uIImage;
            protected set => _uIImage = value;
        }

        #endregion

        #region Delegates

        public delegate void EquipHandler(LivingEntity equipOn);

        public delegate void UnequipHandler(LivingEntity unequipFrom);

        #endregion

        #region Events

        public event EquipHandler EquipEvent;
        public event UnequipHandler UnequipEvent;

        #endregion

        #region Constructor

        protected Item()
        {
            _uIImage = Resources.Load<Sprite>("UI/Items/" + GetType().Name);
            _player = GameManager.Instance.Player;
            _inGameUI = GameManager.Instance.UIManager.MainCanvas.GetComponent<InGameUI>();
        }

        #endregion

        public virtual void Equip(LivingEntity equipOn)
        {
            EquipEvent?.Invoke(equipOn);
        }

        public virtual void Unequip(LivingEntity unequipFrom)
        {
            UnequipEvent?.Invoke(unequipFrom);
        }
    }
}