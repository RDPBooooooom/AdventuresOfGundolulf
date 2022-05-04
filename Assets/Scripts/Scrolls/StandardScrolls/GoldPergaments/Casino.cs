using UnityEngine;
using Random = System.Random;
using PlayerScripts;

namespace Scrolls.StandardScrolls
{
    public class Casino : StandardScroll
    {
        #region Fields

        private Player _player = Managers.GameManager.Instance.Player;
        private int _currentGoldAmount;

        #endregion

        #region Constructor

        public Casino() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _currentGoldAmount = _player.Gold;
            _player.Gold = CalculateNewGold();
        }

        int CalculateNewGold()
        {
            Random random = new Random();
            int chance = random.Next(1, 100);
            if (chance <= 30)
                return _currentGoldAmount * 10;
            else
                return 0;
            
        }

        #endregion
    }
}

