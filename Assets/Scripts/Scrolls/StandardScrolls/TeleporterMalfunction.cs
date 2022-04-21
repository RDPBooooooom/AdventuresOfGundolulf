using System.Collections;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class TeleporterMalfunction : StandardScroll
    {
        #region Fields

        private MonoBehaviourDummy _monoDummy;
        private Bounds _roomBounds;
        private Teleport _teleport;
        private IEnumerator _teleportCoroutine;

        #endregion

        #region Constructor

        public TeleporterMalfunction() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _roomBounds = Managers.GameManager.Instance.LevelManager.CurrentRoom.RoomBounds;
            _teleportCoroutine = Teleport();
            _teleport = Managers.GameManager.Instance.Player.Teleport;
            _monoDummy = Utils.MonoBehaviourDummy.Dummy;
            _monoDummy.StartCoroutine(_teleportCoroutine);

            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        public IEnumerator Teleport()
        {
            while (true)
            {
                yield return new WaitForSeconds(7);
                _teleport.TargetPos = GetRandomPointInRoom();
                _teleport.Use();
            }
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _monoDummy.StopCoroutine(_teleportCoroutine);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        Vector3 GetRandomPointInRoom()
        {
            // Room x length = 33, z length 23. Calculating with diameter of 16 and 11

            Random random = new Random();

            float x = ((float)(random.NextDouble() * (_roomBounds.max.x - _roomBounds.min.x) + _roomBounds.min.x));
            float z = ((float)(random.NextDouble() * (_roomBounds.max.z - _roomBounds.min.z) + _roomBounds.min.z));

            return new Vector3(_roomBounds.center.x + x, 0, _roomBounds.center.z + z);
        }

        #endregion
    }
}
