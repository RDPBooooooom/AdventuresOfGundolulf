using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class NoHitChallenge : StandardScroll
    {
        #region Fields

        private int _currentGoldAmount;
        private int _removedGold;
        private float _playerStartingHP;

        #endregion

        #region Constructor

        public NoHitChallenge() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _currentGoldAmount = Managers.GameManager.Instance.Player.Gold;
            _playerStartingHP = Managers.GameManager.Instance.Player.Health;
            if (_currentGoldAmount >= 20)
                _removedGold = 20;
            else
                _removedGold = _currentGoldAmount;
            Managers.GameManager.Instance.Player.Gold -= _removedGold;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            if(_playerStartingHP == Managers.GameManager.Instance.Player.Health)
                Managers.GameManager.Instance.Player.Gold += _removedGold * 2;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
