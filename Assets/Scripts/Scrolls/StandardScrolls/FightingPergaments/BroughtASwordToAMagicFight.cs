using UnityEngine;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class BroughtASwordToAMagicFight : StandardScroll
    {
        #region Fields

        private Player _player;

        #endregion

        #region Constructor

        public BroughtASwordToAMagicFight() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _player = Managers.GameManager.Instance.Player;
            _player.NotWeeny = true;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _player.NotWeeny = false;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
