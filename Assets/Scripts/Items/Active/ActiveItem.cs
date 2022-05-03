using System.Collections;
using LivingEntities;
using Managers;
using PlayerScripts;
using UnityEngine;
using Utils;

namespace Items.Active
{
    public abstract class ActiveItem : Item
    {
        #region Fields

        protected Timer _cooldown;

        private float _cooldownTime;

        protected UI.InGameUI inGameUI;

        #endregion

        #region Properties

        public float Cooldown
        {
            get => _cooldownTime;
            protected set
            {
                _cooldownTime = value;
                if (_cooldown != null) _cooldown.Time = _cooldownTime;
            }
        }

        #endregion

        #region Constructor

        protected ActiveItem() : base()
        {
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            _player.ActiveItem = this;
            inGameUI = GameManager.Instance.UIManager.MainCanvas.GetComponent<UI.InGameUI>();

            _cooldown = new Timer(equipOn, Cooldown);

            if (equipOn is Player)
            {
                if (_player.ActiveItem != null)
                {
                    GameManager.Instance.ItemManager.ItemSold(_player.ActiveItem);
                    _player.ActiveItem.Unequip(equipOn);
                }
                _player.ActiveItem = this;
            }
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            Debug.Log("Called uneqip");
            if (unequipFrom is Player)
            {
                if(_player.ActiveItem == this)//to make sure you don't remove a newly equipped active item replacing a previous
                    _player.ActiveItem = null;
            }
            
            //_cooldown = null;
        }

        #endregion

        #region Usage

        public void Use()
        {
            Debug.Log(_cooldown);
            if (!_cooldown.IsReady) return;

            GameManager.Instance.Player.Animator.SetTrigger(AnimatorStrings.MagicString);
            Effect();
        }

        protected virtual void Effect()
        {
            _cooldown.Start();
            inGameUI.ItemBackgroundImage.fillAmount = 1;
            MonoBehaviourDummy.Dummy.StartCoroutine(FillUp());
        }
        #endregion

        IEnumerator FillUp()
        {
            yield return new WaitForSeconds(0.25f);
            inGameUI.ItemBackgroundImage.fillAmount -= 0.25f / Cooldown;
            if (inGameUI.ItemBackgroundImage.fillAmount > 0)//0.1 ? to hide Small part which irritates otherwise
                MonoBehaviourDummy.Dummy.StartCoroutine(FillUp());
        }
    }
}
