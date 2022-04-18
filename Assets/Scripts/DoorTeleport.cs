using Assets.Scripts;
using Managers;
using Levels.Rooms;
using PlayerScripts;
using UnityEngine;
using Random = System.Random;
using System.Collections.Generic;

public class DoorTeleport : MonoBehaviour, IInteractable
{
    Player player;
    Room currentRoom;
    List<Room> Rooms;
    public void Interact()
    {
        if(currentRoom.CanLeave())
        {
            Room newRoom = GetRandomRoom();
            player.transform.position = newRoom.transform.position;
            newRoom.Enter();
            Destroy(this);
        }
    }

    void Start()
    {
        player = GameManager.Instance.Player;
        currentRoom = GameManager.Instance.LevelManager.CurrentRoom;
        Rooms = GameManager.Instance.LevelManager.Rooms;
    }

    Room GetRandomRoom()
    {
        Random random = new Random();
        Room randomRoom = Rooms[random.Next(0, Rooms.Count)];
        if (randomRoom != currentRoom)
            return randomRoom;
        else
            return GetRandomRoom();
    }
}
