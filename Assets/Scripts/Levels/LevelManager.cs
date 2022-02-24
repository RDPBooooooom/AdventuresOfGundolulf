using System;
using System.Collections.Generic;
using Levels.Rooms;
using UnityEngine;

namespace Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Room> _roomPrefabs;
        
        [SerializeField] int _numberOfRooms;
        [SerializeField] private bool _showDebug;

        private void Start()
        {
            LevelGenerator gen = new LevelGenerator(_roomPrefabs, _numberOfRooms, _showDebug);
            gen.GenerateLevel();
        }
    }
}