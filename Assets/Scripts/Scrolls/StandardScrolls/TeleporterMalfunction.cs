using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Scrolls.StandardScrolls
{
    public class TeleporterMalfunction : StandardScroll
    {
        Vector3 currentRoomCenter;

        public TeleporterMalfunction()
        {
            Cost = 1;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            currentRoomCenter = Managers.GameManager.Instance.LevelManager.CurrentRoom.transform.position;
            Utils.MonoBehaviourDummy.dummy.StartCoroutine(Teleport());
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        public IEnumerator Teleport()
        {
            Managers.GameManager.Instance.Player.transform.position = GetRandomPointInRoom();
            Debug.Log("Did the telepoo");
            yield return new WaitForSeconds(7);

            Utils.MonoBehaviourDummy.dummy.StartCoroutine(Teleport());
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            Utils.MonoBehaviourDummy.dummy.StopAllCoroutines();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        Vector3 GetRandomPointInRoom()
        {
            // Room x length = 33, z length 23. Calculating with diameter of 16 and 11

            Random random = new Random();
            int x = random.Next(-16, 16);
            int z = random.Next(-11, 11);

            return new Vector3(currentRoomCenter.x + x, 0, currentRoomCenter.z + z);
        }
    }
}
