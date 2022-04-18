using Assets.Scripts.Interfaces;
using System.Collections;
using UnityEngine;
using Utils;

namespace Items.Active
{
    public class Shield : ActiveItem, IUsable
    {
        public Shield() : base()
        {
            Value = 20;
            Cooldown = 30;
        }

        protected override void Effect()
        {
            // Do effect
            MonoBehaviourDummy.Dummy.StartCoroutine(Invincibile());
            _cooldown.Start();
        }

        private IEnumerator Invincibile()
        {
            _player.Invincible = true;
            yield return new WaitForSeconds(3);
            _player.Invincible = false;
        }

        
    }
}