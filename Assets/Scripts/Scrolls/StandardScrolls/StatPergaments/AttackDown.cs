using UnityEngine;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class AttackDown : StandardScroll
    {
        #region Fields

        private Player _player;
        private int _factor = 10;

        #endregion

        #region Constructor

        public AttackDown() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _player = Managers.GameManager.Instance.Player;
            _player.Attack -= _factor;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _player.Attack += _factor;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
