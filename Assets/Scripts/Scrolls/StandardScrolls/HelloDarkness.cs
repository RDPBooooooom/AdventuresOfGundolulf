using UnityEngine;
using Levels.Rooms;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class HelloDarkness : StandardScroll
    {
        #region Fields

        GameObject _fog = Resources.Load<GameObject>("Prefabs/Fog");
        GameObject _gündololülfsVision = Resources.Load<GameObject>("Prefabs/GündololülfsVision");
        GameObject _fogger;
        Room _currentRoom;

        #endregion

        #region Constructor

        public HelloDarkness() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _currentRoom = Managers.GameManager.Instance.LevelManager.CurrentRoom;
            _currentRoom.LeaveRoom += OnLeavingRoom;
            Random random = new Random();
            if(random.Next(0,100)<101)
            {
                _fogger = Object.Instantiate(_gündololülfsVision);
                Debug.Log("Activated Gündololülfs Vision");
            }
            else
            {
                _fogger = Object.Instantiate(_fog);
                Debug.Log("Activated " + GetType().Name);
            }
        }

        private void OnLeavingRoom(Room leaving, Room toEnter)
        {
            _currentRoom.LeaveRoom -= OnLeavingRoom;
            Object.Destroy(_fogger);
        }

        #endregion
    }
}
