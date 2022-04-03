using UnityEngine;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class UnkownPotion : StandardScroll
    {
        PlayerScripts.Player player;
        float previousMaxHealth;
        public UnkownPotion()
        {
            Cost = 0;
        }
        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            player = Managers.GameManager.Instance.Player;
            previousMaxHealth = player.MaxHealth;
            GetNewHealth();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        void GetNewHealth()
        {
            Random random = new Random();
            int chance = random.Next(1, 100);
            float healamount = player.MaxHealth - player.Health;
            if (chance <= 50)
                player.HealEntity(healamount);
            else
            {
                player.MaxHealth %= 5; // should player also loose health down to 5% ? or just not regenerate anymore 
                player.Health = player.MaxHealth;
            }
        }
        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            player.MaxHealth = previousMaxHealth;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
