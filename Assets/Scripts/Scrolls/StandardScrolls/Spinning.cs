using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Scrolls.StandardScrolls
{
    public class Spinning : StandardScroll
    {
        private Camera _cam;
        private float _angleChangeNextStep = 1f;
        private IEnumerator _spinCameraCoroutine;
        private MonoBehaviourDummy _monoDummy;
        
        public Spinning()
        {
            Cost = 3;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _cam = Camera.main;
            _spinCameraCoroutine = SpinCamera();
            _monoDummy =  MonoBehaviourDummy.Dummy;
            _monoDummy.StartCoroutine(_spinCameraCoroutine);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }

        private IEnumerator SpinCamera()
        {
            while (true)
            {
                yield return new WaitForSeconds(0);
                _cam.transform.rotation = Quaternion.Euler(60, 0, _cam.transform.rotation.z + _angleChangeNextStep);
                _angleChangeNextStep+= 0.5f;
            }
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _monoDummy.StopCoroutine(_spinCameraCoroutine);
            //SpinOut();
            Debug.Log(_cam.transform.eulerAngles.z);
            Debug.Log(_cam.transform.rotation.eulerAngles.z);
            _angleChangeNextStep = 0.5f;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        void SpinOut()
        {
            for (float i = _cam.transform.rotation.eulerAngles.z; _cam.transform.rotation.eulerAngles.z > 0.5f; i-= 0.5f)
            {
                Utils.MonoBehaviourDummy.Dummy.StartCoroutine(SpinBack());
            }
            //cam.transform.rotation = Quaternion.Euler(60, 0, 0);
        }

        IEnumerator SpinBack()
        {
            _cam.transform.rotation = Quaternion.Euler(60, 0, _cam.transform.rotation.z - _angleChangeNextStep);
            yield return new WaitForSeconds(0);
        }
    }
}
