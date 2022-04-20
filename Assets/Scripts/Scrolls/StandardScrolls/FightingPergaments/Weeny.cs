using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class Weeny : StandardScroll
    {
        PlayerScripts.Player player;
        public Weeny() : base()
        {

        }

        protected override void ApplyEffect()
        {
            player = Managers.GameManager.Instance.Player;
            player.Weeny = true;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
            Debug.Log("Activated " + GetType().Name);
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            player.Weeny = false;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
