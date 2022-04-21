using UnityEngine;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class SpeedDown : StandardScroll
    {
        #region Fields

        private Player _player;
        private int _factor = 5;

        #endregion

        #region Constructor

        public SpeedDown() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _player = Managers.GameManager.Instance.Player;
            _player.Speed -= _factor;
            inGameUI.UpdateSpeedDisplay();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _player.Speed += _factor;
            inGameUI.UpdateSpeedDisplay();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
