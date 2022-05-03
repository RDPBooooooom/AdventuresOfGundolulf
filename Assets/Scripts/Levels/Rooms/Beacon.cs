using UnityEngine;

namespace Levels.Rooms
{
    public class Beacon : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Light _fire;

        [SerializeField] private Room _currentRoom; //SerializeField for debugging
        [SerializeField] private Room _nextRoom; //SerializeField for debugging

        [SerializeField] private float x;
        [SerializeField] private float z;

        private Color32 _defaultColor = new Color32(30, 220, 210, 255); // blue
        private Color32 _bossRoom = new Color32(255, 0, 0, 255); //Red
        private Color32 _treasureRoom = new Color32(255, 255, 255, 255); //White
        private Color32 _shopRoom = new Color32(0, 155, 20, 255); //green
        private Color32 _startRoom = new Color32(255, 230, 0, 255); //Yellow

        #endregion

        #region Unity Methods

        private void Start()
        {
            _currentRoom =
                transform.parent.parent.GetComponent<Room>(); //Managers.GameManager.Instance.LevelManager.CurrentRoom;
            UpdateColor(_currentRoom);
            _currentRoom.EnterRoom += UpdateColor;
            //_currentRoom.LeaveRoom += UpdateColor;
        }

        #endregion

        #region Color

        void UpdateColor(Room room)
        {
            Vector3 direction = (room.transform.position - transform.position).normalized;
            if (direction.z > 0.8) _nextRoom = room.RoomConnections.Bottom;
            if (direction.z < -0.8) _nextRoom = room.RoomConnections.Top;
            if (direction.x > 0.8) _nextRoom = room.RoomConnections.Left;
            if (direction.x < -0.8) _nextRoom = room.RoomConnections.Right;

            if (_nextRoom is BossRoom)
                _fire.color = _bossRoom;
            else if (_nextRoom is TreasureRoom)
                _fire.color = _treasureRoom;
            else if (_nextRoom is ShopRoom)
                _fire.color = _shopRoom;
            else if (_nextRoom is CombatRoom)
            {
                if (!_nextRoom.Cleared)
                    _fire.color = _defaultColor;
                else
                    TurnOff();
            }
            else if (_nextRoom is StartRoom)
                _fire.color = _startRoom;

            if (_nextRoom == null)
            {
                Debug.Log("Room could not be found", this);
            }
        }

        void TurnOff()
        {
            _fire.gameObject.SetActive(false);
        }

        #endregion
    }
}