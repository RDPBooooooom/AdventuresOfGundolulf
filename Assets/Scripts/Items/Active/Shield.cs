using Assets.Scripts.Interfaces;
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
            _cooldown.Start();
        }

        
    }
}