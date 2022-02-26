using System;
using System.Collections.Generic;
using Levels.Rooms;
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

        private List<Room> _rooms;

        #endregion

        #region Properties

        public Room CurrentRoom { get; private set; }
        public Camera PlayerCam { get; set; }

        #endregion


        private void Awake()
        {
            _levelGenerator = new LevelGenerator(_roomPrefabs, _numberOfRooms, _showDebug);
        }

        public void GenerateLevel()
        {
            _rooms = _levelGenerator.GenerateLevel();
            CurrentRoom = _rooms[Mathf.CeilToInt(Mathf.Sqrt(_numberOfRooms))];
            
            //TODO Event Setup
            
            CurrentRoom.Enter();
        }
        
        
    }
}