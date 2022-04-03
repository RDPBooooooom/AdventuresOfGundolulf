using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class IntelligenceDown : StandardScroll
    {
        PlayerScripts.Player player;
        int factor = 10;
        public IntelligenceDown()
        {
            Cost = 1;
        }
        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            player = Managers.GameManager.Instance.Player;
            player.Intelligence -= factor;
            inGameUI.UpdateIntelligenceDisplay();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            player.Intelligence += factor;
            inGameUI.UpdateIntelligenceDisplay();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
