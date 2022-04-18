using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Scrolls.StandardScrolls
{
    public class Pacifist : StandardScroll
    {
        PlayerScripts.Player player;
        public Pacifist() : base()
        {

        }

        protected override void ApplyEffect()
        {
            player = Managers.GameManager.Instance.Player;
            MonoBehaviourDummy.Dummy.StartCoroutine(NoOuchAllowed());
            Debug.Log("Activated " + GetType().Name);
        }

        IEnumerator NoOuchAllowed()
        {
            player.Pacifist = true;
            yield return new WaitForSeconds(10);
            player.Pacifist = false;
        }
    }
}
