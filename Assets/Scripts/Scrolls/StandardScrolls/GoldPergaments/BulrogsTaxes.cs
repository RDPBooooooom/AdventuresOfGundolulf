using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class BulrogsTaxes : StandardScroll
    {
        public BulrogsTaxes() : base()
        {
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            CalculateNewGold();
        }

        void CalculateNewGold()
        {
            Managers.GameManager.Instance.Player.Gold = Mathf.CeilToInt(Managers.GameManager.Instance.Player.Gold /2);
        }
    }
}
