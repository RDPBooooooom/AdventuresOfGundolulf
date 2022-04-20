using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class BroughtASwordToAMagicFight : StandardScroll
    {
        PlayerScripts.Player player;
        public BroughtASwordToAMagicFight() : base()
        {
        }

        protected override void ApplyEffect()
        {
            player = Managers.GameManager.Instance.Player;
            player.NotWeeny = true;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
            Debug.Log("Activated " + GetType().Name);
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            player.NotWeeny = false;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
