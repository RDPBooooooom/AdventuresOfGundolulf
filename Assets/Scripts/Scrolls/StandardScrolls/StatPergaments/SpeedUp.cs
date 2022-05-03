using PlayerScripts;
using UnityEngine;

namespace Scrolls.StandardScrolls.StatPergaments
{
    public class SpeedUp : StandardScroll
    {
        #region Fields

        private Player _player;
        private int _factor = 5;

        #endregion

        #region Constructor

        public SpeedUp() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _player = Managers.GameManager.Instance.Player;
            _player.Speed += _factor;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _player.Speed -= _factor;
            leaving.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
