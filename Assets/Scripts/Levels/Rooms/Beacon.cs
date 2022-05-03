using UnityEngine;
using Levels.Rooms;

public class Beacon : MonoBehaviour
{
    #region Fields

    [SerializeField] private Light _fire;

    [SerializeField]private Room _currentRoom;    //SerializeField for debugging
    [SerializeField]private Room _nextRoom;       //SerializeField for debugging

    private Color32 _defaultColor = new Color32(30, 220, 210,255); // blue
    private Color32 _bossRoom = new Color32(255, 0, 0, 255); //Red
    private Color32 _treasureRoom = new Color32(255, 255, 255, 255); //White
    private Color32 _shopRoom = new Color32(0, 155, 20, 255); //green
    private Color32 _startRoom = new Color32(255, 230, 0, 255); //Yellow

    #endregion

    #region Unity Methods

    private void Start()
    {
        _currentRoom = transform.parent.parent.GetComponent<Room>(); //Managers.GameManager.Instance.LevelManager.CurrentRoom;
        UpdateColor(_currentRoom);
        _currentRoom.EnterRoom += UpdateColor;
        //_currentRoom.LeaveRoom += UpdateColor;
    }

    #endregion

    #region Color
    void UpdateColor(Room room)
    {
        switch (transform.eulerAngles.y)
        {
            case 180:
                _nextRoom = _currentRoom.RoomConnections.Bottom;
                break;

            case 90:
                _nextRoom = _currentRoom.RoomConnections.Left;
                break;

            case 270:
                _nextRoom = _currentRoom.RoomConnections.Right;
                break;

            default:
                _nextRoom = _currentRoom.RoomConnections.Top;
                break;
        }
        if (_nextRoom is Levels.Rooms.BossRoom)
            _fire.color = _bossRoom;
        else if (_nextRoom is Levels.Rooms.TreasureRoom)
            _fire.color = _treasureRoom;
        else if (_nextRoom is Levels.Rooms.ShopRoom)
            _fire.color = _shopRoom;
        else if (_nextRoom is CombatRoom)
        {
            if(!_nextRoom.Cleared)
                _fire.color = _defaultColor;
             else
                TurnOff();
        }
        else if(_nextRoom is StartRoom)
            _fire.color = _startRoom;

        if(_nextRoom == null)
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
