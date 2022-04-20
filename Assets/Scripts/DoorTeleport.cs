using Managers;
using Levels.Rooms;
using PlayerScripts;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;
using Assets.Scripts;

public class DoorTeleport : MonoBehaviour, IInteractable
{
    private Player _player;
    private LevelManager _levelManager;
    private List<Room> _rooms;
    public void Interact()
    {
        if(_levelManager.CurrentRoom.CanLeave())
        {
            
            Room newRoom = GetRandomRoom();
            _levelManager.CurrentRoom.Leave(newRoom);
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        _player = GameManager.Instance.Player;
        _levelManager = GameManager.Instance.LevelManager;
        _rooms = GameManager.Instance.LevelManager.Rooms;
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
}
