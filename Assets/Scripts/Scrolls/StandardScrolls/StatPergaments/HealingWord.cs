using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HealingWord : StandardScroll
    {
        public HealingWord()
        {
            Cost = -2;
        }
        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            Managers.GameManager.Instance.Player.HealEntity(50);
        }
    }
}
