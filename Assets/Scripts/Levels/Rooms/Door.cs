using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Levels.Rooms
{
    public class Door : MonoBehaviour
    {
        #region Fields

        private Room _room;
        private static Timer _doorTimer;

        #endregion

        #region Delegates

        public delegate void EnteredDoorHandler(Door door);

        #endregion

        #region Events

        public event EnteredDoorHandler OnDoorEntry;

        #endregion

        #region Unity Methods

        void Start()
        {
            _doorTimer ??= new Timer(MonoBehaviourDummy.Dummy, 1);

            _room = GetComponentInParent<Room>();
            _room.RegisterDoor(this);
            _room.RoomCleared += OpenDoors;
            _room.RoomCleared += SetupCleared;

            SceneManager.activeSceneChanged += CleanUp;
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!_doorTimer.IsReady) return;
        
            Player player = other.GetComponent<Player>();
            if (player == null) return;

            _doorTimer.Start();
            OnDoorEntry?.Invoke(this);
        }

        #endregion

        #region Door Methods

        private void SetupCleared()
        {
            _room.RoomCleared -= OpenDoors;
            _room.RoomCleared -= SetupCleared;
            _room.EnterRoom += OpenDoors;
            _room.LeaveRoom += CloseDoors;
        }


        private void OpenDoors(Room entering)
        {
            OpenDoors();
        }

        private void OpenDoors()
        {
            transform.GetChild(0).Rotate(0, 90, 0);
        
        }

        private void CloseDoors(Room leaving, Room entering)
        {
            CloseDoors();
        }

        private void CloseDoors()
        {
            transform.GetChild(0).Rotate(0, -90, 0);
        }

        private void CleanUp(Scene current, Scene next)
        {
            _doorTimer = null;
            SceneManager.activeSceneChanged -= CleanUp;
        }

        #endregion
    }
}