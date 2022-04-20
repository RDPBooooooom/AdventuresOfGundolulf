using UnityEngine;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class Casino : StandardScroll
    {
        int currentGoldAmount;
        PlayerScripts.Player player = Managers.GameManager.Instance.Player;
        public Casino() : base()
        {
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            currentGoldAmount = player.Gold;
            player.Gold = CalculateNewGold();
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

