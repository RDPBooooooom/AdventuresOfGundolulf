using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class TopUpPerspective : StandardScroll
    {
        Camera cam;
        public TopUpPerspective() : base()
        {
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            cam = Camera.main;
            FlipCamera();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        void FlipCamera()
        {
            cam.transform.rotation = Quaternion.Euler(60, 0, 180);
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            cam.transform.rotation = Quaternion.Euler(60, 0, 0);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
