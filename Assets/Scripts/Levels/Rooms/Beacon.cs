using UnityEngine;
using Levels.Rooms;

public class Beacon : MonoBehaviour
{
    #region Fields

    [SerializeField] private Light _fire;

    private Room _currentRoom;
    private Room _nextRoom;

    private Color32 _defaultColor = new Color32(30, 220, 210,255); // blue
    private Color32 _bossRoom = new Color32(255, 0, 0, 255); //Red
    private Color32 _treasureRoom = new Color32(255, 255, 255, 255); //White
    private Color32 _shopRoom = new Color32(0, 155, 20, 255); //green

    private bool _startUpCall = true;

    #endregion

    #region Unity Methods

    private void Start()
    {
        _currentRoom = Managers.GameManager.Instance.LevelManager.CurrentRoom;
        UpdateColor(_currentRoom, _currentRoom);
        _startUpCall = false;
    }

    #endregion

    #region Color

    //[ContextMenu("Test")]
    void UpdateColor(Room oldroom, Room room)
    {
        _currentRoom = room;
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
        if(_nextRoom is Levels.Rooms.BossRoom)
            _fire.color = _bossRoom;
        else if(_nextRoom is Levels.Rooms.TreasureRoom)
            _fire.color = _treasureRoom;
        else if(_nextRoom is Levels.Rooms.ShopRoom)
            _fire.color = _shopRoom;
        else
            _fire.color = _defaultColor;

        //if(!_startUpCall)
        //{
            _currentRoom.LeaveRoom += UpdateColor;
        if(_nextRoom is CombatRoom)
            _nextRoom.RoomCleared += TurnOff;
        //}
        //else{}

    }

    void TurnOff()
    {
        Debug.Log("Room was cleared");
        _fire.gameObject.SetActive(false);
    }

    #endregion
}
