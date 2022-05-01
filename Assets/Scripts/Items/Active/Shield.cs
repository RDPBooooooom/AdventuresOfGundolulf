using Assets.Scripts.Interfaces;
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