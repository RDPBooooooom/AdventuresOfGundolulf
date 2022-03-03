using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Levels.Rooms
{
    public abstract class Room : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool _wasVisited;
        [SerializeField] private RoomData _data;
        [SerializeField] private GameObject _floorPlane;

        private Bounds _roomBounds;

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

        protected void Start()
        {
            _roomBounds = _floorPlane.GetComponent<MeshFilter>().mesh.bounds;
            _roomBounds.extents = Vector3.Scale(_roomBounds.extents, _floorPlane.transform.localScale * 0.95f);
            _roomBounds.extents += new Vector3(0, 5, 0);
            _roomBounds.center = _floorPlane.transform.position;
        }

        #endregion

        #region Door Management

        public void RegisterDoor(Door door)
        {
            Vector3 direction = (transform.position - door.transform.position).normalized;

            if (direction.z > 0.8) _doors.Add(door, DoorDirections.Bottom);
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

        #region Helper Methods

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

        public Vector3 GetClosestPositionInRoom(Vector3 position)
        {
            if (IsPositionInRoom(position)) return position;

            return _roomBounds.ClosestPoint(position);
        }

        public Vector3 GetClosestPositionOnGround(Vector3 position)
        {
            Debug.Log("Got Pos: " + position);
            Vector3 pos = GetClosestPositionInRoom(position);
            Debug.Log("Found Pos: " + pos);
            
            return new Vector3(pos.x, 0, pos.z);

        }

        public bool IsPositionInRoom(Vector3 position)
        {
            return _roomBounds.Contains(position);
        }


        #endregion
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