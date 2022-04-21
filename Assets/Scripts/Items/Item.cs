using LivingEntities;
using UnityEngine;
using PlayerScripts;
using Managers;
using Effects;
using System.Collections.Generic;
using UI;

namespace Items
{
    public class Item
    {
        #region Fields

        private Sprite _uIImage;
        private int _value;

        protected Player _player;
        protected InGameUI _inGameUI = GameManager.Instance.UIManager.MainCanvas.GetComponent<InGameUI>();

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
        }

        #endregion

        #region Equip

        public virtual void Equip(LivingEntity equipOn)
        {
            EquipEvent?.Invoke(equipOn);
        }

        public virtual void Unequip(LivingEntity unequipFrom)
        {
            UnequipEvent?.Invoke(unequipFrom);
        }

        #endregion
    }
}