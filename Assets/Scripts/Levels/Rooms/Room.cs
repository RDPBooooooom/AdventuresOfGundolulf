using System;
using Levels.Rooms;
using UnityEngine;
using PlayerScripts;

namespace Levels.Rooms
{
    public abstract class Room : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _wasVisited;
        [SerializeField] private RoomData _data;

        #endregion
        
        #region Properties
        public bool WasVisited
        {
            get => _wasVisited;
            protected set => _wasVisited = value;
        }

        public RoomData Data
        {
            get => _data;
            set => _data = value;
        }
        
        #endregion

        #region Delegates

        public delegate void EnterRoomHandler(Room entering);

        public delegate void LeaveRoomHandler(Room leaving);

        #endregion
        
        #region Events

        public event EnterRoomHandler EnterRoom;
        public event LeaveRoomHandler LeaveRoom;

        #endregion

        #region UnityMethods

        #endregion

        #region Room Triggers

        public virtual void Enter()
        {
            EnterRoom?.Invoke(this);
        }

        public virtual void Leave()
        {
            LeaveRoom?.Invoke(this);
        }
        #endregion
        
    }

    [Flags]
    public enum DoorDirections
    {
        bottom = 1,
        top = 2,
        left = 4,
        right = 8
    }
}