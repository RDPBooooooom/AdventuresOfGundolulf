using System.Collections;
using LivingEntities;
using Managers;
using UnityEngine;
using Utils;

namespace Items.Active
{
    public abstract class ActiveItem : Item
    {
        #region Fields

        protected Timer _cooldown;
        private float _cooldownTime;
        UI.InGameUI inGameUI = GameManager.Instance.UIManager.MainCanvas.GetComponent<UI.InGameUI>();

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
        
        protected ActiveItem() : base()
        {
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            _cooldown = new Timer(equipOn, Cooldown);
            Debug.Log(ToString().Remove(0, 13));
            inGameUI.UpdateActiveItem(ToString().Remove(0, 13));
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            _cooldown = null;
            inGameUI.UpdateActiveItem("");
        }
        
        public void Use()
        {
            if (!_cooldown.IsReady) return;
            GameManager.Instance.Player.Animator.SetTrigger(AnimatorStrings.MagicString);
            Effect();
        }

        protected abstract void Effect();
    }
}
