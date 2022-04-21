using Managers;
using Levels.Rooms;
using PlayerScripts;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;
using Assets.Scripts;

public class DoorTeleport : MonoBehaviour, IInteractable
{
    #region Fields

    private Player _player;
    private LevelManager _levelManager;
    private List<Room> _rooms;

    #endregion

    #region Unity Methods

    void Start()
    {
        _player = GameManager.Instance.Player;
        _levelManager = GameManager.Instance.LevelManager;
        _rooms = GameManager.Instance.LevelManager.Rooms;
    }

    #endregion

    #region Effect

    public void Interact()
    {
        if(_levelManager.CurrentRoom.CanLeave())
        {
            
            Room newRoom = GetRandomRoom();
            _levelManager.CurrentRoom.Leave(newRoom);
            Destroy(gameObject);
        }
    }

    Room GetRandomRoom()
    {
        Random random = new Random();
        Room randomRoom = _rooms[random.Next(0, _rooms.Count)];
        if (randomRoom != _levelManager.CurrentRoom)
            return randomRoom;
        else
            return GetRandomRoom();
    }

    #endregion
}
