using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Scrolls.StandardScrolls
{
    public class HolyBlessing : StandardScroll
    {
        PlayerScripts.Player player;
        public HolyBlessing() : base()
        {

        }

        protected override void ApplyEffect()
        {
            player = Managers.GameManager.Instance.Player;
            MonoBehaviourDummy.Dummy.StartCoroutine(Invincible());
            Debug.Log("Activated " + GetType().Name);
        }

        IEnumerator Invincible()
        {
            player.Invincible = true;
            yield return new WaitForSeconds(10);
            player.Invincible = false;
        }
    }
}
