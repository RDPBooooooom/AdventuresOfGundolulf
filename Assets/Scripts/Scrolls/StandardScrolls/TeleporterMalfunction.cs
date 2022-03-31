using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class TeleporterMalfunction : StandardScroll
    {
        private Vector3 _currentRoomCenter;
        private IEnumerator _teleportCoroutine;
        private MonoBehaviourDummy _monoDummy;

        public TeleporterMalfunction()
        {
            Cost = 1;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _currentRoomCenter = Managers.GameManager.Instance.LevelManager.CurrentRoom.transform.position;
            _teleportCoroutine = Teleport();
            _monoDummy = Utils.MonoBehaviourDummy.Dummy;
            _monoDummy.StartCoroutine(_teleportCoroutine);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        public IEnumerator Teleport()
        {
            while (true)
            {
                yield return new WaitForSeconds(7);
                Managers.GameManager.Instance.Player.transform.position = GetRandomPointInRoom();
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
            int x = random.Next(-16, 16);
            int z = random.Next(-11, 11);

            return new Vector3(_currentRoomCenter.x + x, 0, _currentRoomCenter.z + z);
        }
    }
}
