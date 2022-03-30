using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class Spinning : StandardScroll
    {
        Camera cam;
        public Spinning()
        {
            Cost = 3;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            cam = Camera.main;
            Utils.MonoBehaviourDummy.Dummy.StartCoroutine(SpinCamera());
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        IEnumerator SpinCamera() // Currently gives an error because it breaks calculations with rays from the camera in Player
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, cam.transform.rotation.z + 1);
            yield return new WaitForSeconds(0.5f);

            Utils.MonoBehaviourDummy.Dummy.StartCoroutine(SpinCamera());
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            Utils.MonoBehaviourDummy.Dummy.StopCoroutine(SpinCamera());
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
