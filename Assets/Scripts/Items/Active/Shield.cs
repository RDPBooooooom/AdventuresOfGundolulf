using Assets.Scripts.Interfaces;
using LivingEntities;
using System.Collections;
using UnityEngine;
using Utils;

namespace Items.Active
{
    public class Shield : ActiveItem, IUsable
    {
        #region Constructor

        public Shield() : base()
        {
            Value = 20;
            Cooldown = 30;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
        }

        #endregion

        #region Effect

        protected override void Effect()
        {
            MonoBehaviourDummy.Dummy.StartCoroutine(Invincibile());
            base.Effect();
        }

        private IEnumerator Invincibile()
        {
            _player.Invincible = true;
            yield return new WaitForSeconds(3);
            _player.Invincible = false;
        }

        #endregion
    }
}