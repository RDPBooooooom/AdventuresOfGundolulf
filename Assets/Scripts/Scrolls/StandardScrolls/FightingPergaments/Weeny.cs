using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class Weeny : StandardScroll
    {
        #region Fields

        private Player _player;

        #endregion

        #region Constructor

        public Weeny() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _player = Managers.GameManager.Instance.Player;
            _player.Weeny = true;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _player.Weeny = false;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
