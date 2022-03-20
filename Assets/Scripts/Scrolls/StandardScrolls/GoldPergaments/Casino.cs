using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class Casino : StandardScroll
    {
        int currentGoldAmount;
        public Casino()
        {
            Cost = 2;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            currentGoldAmount = Managers.GameManager.Instance.Player.Gold;
            CalculateNewGold();
        }

        int CalculateNewGold()
        {
            Random random = new Random();
            int chance = random.Next(1, 100);
            if (chance <= 30)
                return currentGoldAmount * 10;
            else
                return 0;
            
        }
    }
}

