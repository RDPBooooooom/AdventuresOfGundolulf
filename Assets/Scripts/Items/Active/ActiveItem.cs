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

            _cooldown = new Timer(equipOn, Cooldown);
            _inGameUI.UpdateActiveItem(UIImage);
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _cooldown = null;
            _inGameUI.UpdateActiveItem(null);
        }

        #endregion

        #region Usage

        public void Use()
        {
            if (!_cooldown.IsReady) return;

            GameManager.Instance.Player.Animator.SetTrigger(AnimatorStrings.MagicString);
            Effect();
        }

        protected abstract void Effect();

        #endregion
    }
}
