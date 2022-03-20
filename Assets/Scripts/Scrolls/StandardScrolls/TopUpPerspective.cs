using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class TopUpPerspective : StandardScroll
    {
        Camera cam;
        public TopUpPerspective()
        {
            Cost = 2;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            cam = Camera.main;
            FlipCamera();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        void FlipCamera()// Currently gives an error because it breaks calculations with rays from the camera in Player
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
