using System.Collections;
using UnityEngine;
using Utils;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class Pacifist : StandardScroll
    {
        #region Fields
        
        private Player _player;

        #endregion

        #region Constructor

        public Pacifist() : base()
        {
        }

        #endregion

        #region Effect
        
        protected override void ApplyEffect()
        {
            _player = Managers.GameManager.Instance.Player;
            MonoBehaviourDummy.Dummy.StartCoroutine(NoOuchAllowed());
            Debug.Log("Activated " + GetType().Name);
        }

        IEnumerator NoOuchAllowed()
        {
            _player.Pacifist = true;
            yield return new WaitForSeconds(10);
            _player.Pacifist = false;
        }

        #endregion
    }
}
