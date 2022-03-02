using System;
using System.Collections;
using System.Collections.Generic;
using Levels.Rooms;
using PlayerScripts;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Delegates

    public delegate void EnteredDoorHandler(Door door);

    #endregion

    #region Events

    public event EnteredDoorHandler OnDoorEntry;

    #endregion

    #region Unity Methods

    void Start()
    {
        Room room = GetComponentInParent<Room>();
        room.RegisterDoor(this);
        room.RoomCleared += OpenDoors;
    }

    #endregion
    
    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        OnDoorEntry?.Invoke(this);
    }

    private void OpenDoors()
    {
        transform.GetChild(0).Rotate(0,90,0);
    }
}