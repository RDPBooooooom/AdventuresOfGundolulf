using UnityEngine;
using Random = System.Random;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class UnknownPotion : StandardScroll
    {
        #region Fields

        private Player _player;
        private float _previousMaxHealth;

        #endregion

        #region Constructor

        public UnknownPotion() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _player = Managers.GameManager.Instance.Player;
            _previousMaxHealth = _player.MaxHealth;
            GetNewHealth();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        void GetNewHealth()
        {
            Random random = new Random();
            int chance = random.Next(1, 100);
            float healamount = _player.MaxHealth - _player.Health;
            if (chance <= 50)
                _player.HealEntity(healamount);
            else
            {
                //_player.MaxHealth *= 0.1f;
                _player.DamageEntity(_player.Health*0.9f);
            }
        }
        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _player.MaxHealth = _previousMaxHealth;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
