using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Levels.Rooms
{
    public abstract class Room : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _wasVisited;
        [SerializeField] private RoomData _data;
        
        private Dictionary<Door, DoorDirections> _doors;

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
        
        
        public RoomConnections RoomConnections { get; set; }
        
        #endregion

        #region Delegates

        public delegate void EnterRoomHandler(Room entering);
        
        public delegate void RoomClearedHandler();
        
        public delegate void LeaveRoomHandler(Room leaving, Room leavingTo);

        #endregion
        
        #region Events

        public event EnterRoomHandler EnterRoom;
        
        public event RoomClearedHandler RoomCleared;
        public event LeaveRoomHandler LeaveRoom;

        #endregion

        #region UnityMethods

        protected void Awake()
        {
            _doors = new Dictionary<Door, DoorDirections>();
        }

        #endregion

        #region Door Management

        public void RegisterDoor(Door door)
        {
            Vector3 direction = (transform.position - door.transform.position).normalized;
            
            if(direction.z > 0.8) _doors.Add(door, DoorDirections.Bottom);
            if (direction.z < -0.8) _doors.Add(door, DoorDirections.Top);
            if (direction.x > 0.8) _doors.Add(door, DoorDirections.Left);
            if (direction.x < -0.8) _doors.Add(door, DoorDirections.Right);

            door.OnDoorEntry += TryLeave;
        }

        #endregion

        #region Room Triggers

        public virtual void Enter()
        {
            EnterRoom?.Invoke(this);
        }

        public virtual void OnRoomCleared()
        {
            RoomCleared?.Invoke();
        }

        public void TryLeave(Door door)
        {
            if (CanLeave())
            {
                _wasVisited = true;
                Room toEnter = GetRoomByDirection(_doors[door]);
                LeaveRoom?.Invoke(this, toEnter);
                Debug.Log("Leaving " + name);
            }
        }

        protected virtual bool CanLeave()
        {
            return true;
        }
        #endregion


        private Room GetRoomByDirection(DoorDirections direction)
        {
            Debug.Log(direction);
            switch (direction)
            {
             case DoorDirections.Top:
                 return RoomConnections.Top;
             case DoorDirections.Bottom:
                 return RoomConnections.Bottom;
             case DoorDirections.Right:
                 return RoomConnections.Right;
             case DoorDirections.Left:
                 return RoomConnections.Left;
             default:
                 return null;
            }
        }
    }

    [Flags]
    public enum DoorDirections
    {
        Bottom = 1,
        Top = 2,
        Left = 4,
        Right = 8
    }
}