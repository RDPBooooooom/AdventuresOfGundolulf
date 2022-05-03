using System.Collections;
using UnityEngine;
using Utils;

namespace Scrolls.StandardScrolls
{
    public class Spinning : StandardScroll
    {
        #region Fields

        private MonoBehaviourDummy _monoDummy;
        private Camera _cam;
        private Quaternion _defaultRotation = Quaternion.Euler(60, 0, 0);
        private IEnumerator _spinCameraCoroutine;

        private float _angleChangeNextStep = 1f;

        #endregion

        #region Constructor

        public Spinning() : base()
        {
        }

        #endregion

        #region Effect

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
                _angleChangeNextStep += 0.125f;
            }
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _monoDummy.StopCoroutine(_spinCameraCoroutine);
            Debug.Log(_cam.transform.eulerAngles.z);
            _angleChangeNextStep = 0.5f;
            _monoDummy.StartCoroutine(SpinBack());
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        IEnumerator SpinBack()
        {
            //float spinValue = 0.5f;
            //while(_cam.transform.rotation.z > 0)
            //{
                yield return new WaitForSeconds(0);
            //    Debug.Log(_cam.transform.rotation.z);
            //    _cam.transform.rotation = Quaternion.Euler(60,0,_cam.transform.rotation.z -spinValue);
            //    Debug.Log("SpanBack");
            //    spinValue += 0.5f;
            //}
            _cam.transform.rotation = _defaultRotation;
        }

        #endregion
    }
}
