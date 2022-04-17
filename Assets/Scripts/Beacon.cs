using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Levels.Rooms;

public class Beacon : MonoBehaviour
{
    Room currentRoom;
    [SerializeField] Room nextRoom;
    [SerializeField] Light fire;

    Color32 defaultColor = new Color32(30, 220, 210,255); // blue
    Color32 bossRoom = new Color32(255, 0, 0, 255); //Red
    Color32 treasureRoom = new Color32(255, 255, 255, 255); //White
    Color32 shopRoom = new Color32(0, 155, 20, 255); //green

    bool startUpCall = true;

    private void Start()
    {
        currentRoom = Managers.GameManager.Instance.LevelManager.CurrentRoom;
        UpdateColor(currentRoom);
        startUpCall = false;
    }

    [ContextMenu("Test")]
    void UpdateColor(Room room)
    {
        currentRoom = room;
        switch (transform.eulerAngles.y)
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

        if(!startUpCall)
        {
            nextRoom.EnterRoom += UpdateColor;
            nextRoom.RoomCleared += TurnOff;
        }
        else{}

    }

    void TurnOff()
    {
        Debug.Log("Room was cleared");
        fire.gameObject.SetActive(false);
    }
}
