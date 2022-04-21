using UnityEngine;
using Levels.Rooms;

namespace Scrolls.StandardScrolls
{
    public class HelloDarkness : StandardScroll
    {
        #region Fields

        GameObject _fog = Resources.Load<GameObject>("Prefabs/Fog");
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
            _fogger = Object.Instantiate(_fog, new Vector3(_currentRoom.transform.position.x, 6.5f ,_currentRoom.transform.position.z), Quaternion.identity);
            Debug.Log("Activated " + GetType().Name);
        }

        private void OnLeavingRoom(Room leaving, Room toEnter)
        {
            _currentRoom.LeaveRoom -= OnLeavingRoom;
            Object.Destroy(_fogger);
        }

        #endregion
    }
}
