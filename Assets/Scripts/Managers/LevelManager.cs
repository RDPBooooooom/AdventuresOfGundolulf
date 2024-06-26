﻿using System.Collections.Generic;
using System.Linq;
using Levels;
using Levels.Rooms;
using PlayerScripts;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<Room> _roomPrefabs;
        [SerializeField] private int _numberOfRooms;
        [SerializeField] private bool _showDebug;
        [SerializeField] private int _numberOfShopRooms = 20;
        [SerializeField] private int _numberIfTreasureRooms = 1;
        [SerializeField] private int _distanceToBossRoom = 4;

        Room _currentRoom;

        private LevelGenerator _levelGenerator;

        #endregion

        #region Properties

        public List<Room> Rooms { get; private set; }

        public Room CurrentRoom 
        {
            get =>_currentRoom;
            private set
            {
                _currentRoom = value;
                OnChangeRoomEvent?.Invoke(value);
            }
        }

        public Camera PlayerCam { get; set; }

        #endregion

        #region Events
        public delegate void CurrentRoomHandler(Room room);
        public event CurrentRoomHandler OnChangeRoomEvent;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            _levelGenerator = new LevelGenerator(_roomPrefabs, _numberOfRooms, _showDebug, _numberOfShopRooms,
                _numberIfTreasureRooms, _distanceToBossRoom);
            PlayerCam = Camera.main;
        }

        #endregion

        #region Level Generation

        public void GenerateLevel()
        {
            Rooms = _levelGenerator.GenerateLevel();

            Room[] startRooms = Rooms.Where(r => r.GetType() == typeof(StartRoom)).ToArray();

            if (startRooms.Length > 0)
            {
                CurrentRoom = startRooms[0];
            }
            else
            {
                CurrentRoom = Rooms[0];
            }

            RoomSetup();
        }

        #endregion

        #region Room Methods

        private void OnLeavingRoom(Room leaving, Room toEnter)
        {
            ClearSetup();
            CurrentRoom = toEnter;

            Player player = GameManager.Instance.Player;
            player.transform.position = toEnter.GetClosestPositionOnGround(player.transform.position);

            Vector3 pos1 = leaving.transform.position;
            Vector3 pos2 = toEnter.transform.position;
            PlayerCam.transform.position += new Vector3(pos2.x - pos1.x, 0, pos2.z - pos1.z);

            RoomSetup();
        }

        private void RoomSetup()
        {
            CurrentRoom.Enter();
            CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private void ClearSetup()
        {
            CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}