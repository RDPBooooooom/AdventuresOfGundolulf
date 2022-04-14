using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels.Rooms;

public class Beacon : MonoBehaviour
{
    Room currentRoom;
    Room nextRoom;
    [SerializeField] Light fire;

    Color defaultColor = new Color(30, 220, 210); // blue
    Color bossRoom = new Color(255, 0, 0); //Red
    Color treasureRoom = new Color(255, 255, 255); //White
    Color shopRoom = new Color(0, 155, 20); //green

    [ContextMenu("Test")]
    void Start()
    {
        currentRoom = Managers.GameManager.Instance.LevelManager.CurrentRoom;
        switch (transform.rotation.y)
        {
            case 180:
                nextRoom = currentRoom.RoomConnections.Bottom;
                break;

            case 90:
                nextRoom = currentRoom.RoomConnections.Left;
                break;

            case 270:
                nextRoom = currentRoom.RoomConnections.Right;
                break;

            default:
                nextRoom = currentRoom.RoomConnections.Top;
                break;
        }
        if(nextRoom is Levels.Rooms.BossRoom)
            fire.color = bossRoom;
        else if(nextRoom is Levels.Rooms.TreasureRoom)
            fire.color = treasureRoom;
        else if(nextRoom is Levels.Rooms.ShopRoom)
            fire.color = shopRoom;
        else
            fire.color = defaultColor;

        Debug.Log(nextRoom is Levels.Rooms.TreasureRoom);
        
        nextRoom.RoomCleared += TurnOff;
    }

    void TurnOff()
    {
        fire.gameObject.SetActive(false);
    }
}
