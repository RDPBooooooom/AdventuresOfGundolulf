using System;
using System.Collections.Generic;
using Levels.Rooms;
using Managers;
using PlayerScripts;
using UnityEngine;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<Room> _roomPrefabs;
        [SerializeField] int _numberOfRooms;
        [SerializeField] private bool _showDebug;

        private LevelGenerator _levelGenerator;

        #endregion

        #region Properties
        public List<Room> Rooms { get; private set; }
        public Room CurrentRoom { get; private set; }
        public Camera PlayerCam { get; set; }

        #endregion


        private void Awake()
        {
            _levelGenerator = new LevelGenerator(_roomPrefabs, _numberOfRooms, _showDebug);
        }

        public void GenerateLevel()
        {
            Rooms = _levelGenerator.GenerateLevel();
            CurrentRoom = Rooms[Mathf.CeilToInt(Mathf.Sqrt(_numberOfRooms))];
            
            RoomSetup();
        }

        private void OnLeavingRoom(Room leaving, Room toEnter)
        {
            ClearSetup();
            CurrentRoom = toEnter;

            //TODO Move Character and Cam in a good way, This is WIP
            FindObjectOfType<Player>().transform.position = toEnter.transform.position;
            Vector3 pos1 = leaving.transform.position;
            Vector3 pos2 = toEnter.transform.position;
            Camera.main.transform.position += new Vector3(pos2.x - pos1.x, 0, pos2.z - pos1.z );
            
            
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


    }
}