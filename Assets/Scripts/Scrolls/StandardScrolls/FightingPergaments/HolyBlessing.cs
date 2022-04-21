using System.Collections;
using UnityEngine;
using Utils;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class HolyBlessing : StandardScroll
    {
        #region Fields

        private Player _player;

        #endregion

        #region Constructor

        public HolyBlessing() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _player = Managers.GameManager.Instance.Player;
            MonoBehaviourDummy.Dummy.StartCoroutine(Invincible());
            Debug.Log("Activated " + GetType().Name);
        }

        IEnumerator Invincible()
        {
            _player.Invincible = true;
            yield return new WaitForSeconds(10);
            _player.Invincible = false;
        }

        #endregion
    }
}
