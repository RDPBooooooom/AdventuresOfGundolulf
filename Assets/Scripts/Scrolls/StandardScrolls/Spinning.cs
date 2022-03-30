using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class Spinning : StandardScroll
    {
        Camera cam;
        float i = 0.5f;
        public Spinning()
        {
            Cost = 3;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            cam = Camera.main;
            Utils.MonoBehaviourDummy.dummy.StartCoroutine(SpinCamera());
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        IEnumerator SpinCamera()
        {
            cam.transform.rotation = Quaternion.Euler(60, 0, cam.transform.rotation.z + i);
            i+= 0.5f;
            yield return new WaitForSeconds(0);

            Utils.MonoBehaviourDummy.dummy.StartCoroutine(SpinCamera());
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            Utils.MonoBehaviourDummy.dummy.StopCoroutine(SpinCamera());
            Utils.MonoBehaviourDummy.dummy.StopAllCoroutines();
            //SpinOut();
            Debug.Log(cam.transform.eulerAngles.z);
            Debug.Log(cam.transform.rotation.eulerAngles.z);
            i = 0.5f;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        void SpinOut()
        {
            for (float i = cam.transform.rotation.eulerAngles.z; cam.transform.rotation.eulerAngles.z > 0.5f; i-= 0.5f)
            {
                Utils.MonoBehaviourDummy.dummy.StartCoroutine(SpinBack());
            }
            //cam.transform.rotation = Quaternion.Euler(60, 0, 0);
        }

        IEnumerator SpinBack()
        {
            cam.transform.rotation = Quaternion.Euler(60, 0, cam.transform.rotation.z - i);
            yield return new WaitForSeconds(0);
        }
    }
}
