using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class SpeedUp : StandardScroll
    {
        PlayerScripts.Player player;
        int factor = 5;
        public SpeedUp()
        {
            Cost = 1;
        }
        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            player = Managers.GameManager.Instance.Player;
            player.Speed += factor;
            inGameUI.UpdateSpeedDisplay();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            player.Speed -= factor;
            inGameUI.UpdateSpeedDisplay();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
