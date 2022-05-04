using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class TopUpPerspective : StandardScroll
    {
        #region Fields

        Camera _cam;

        #endregion

        #region Constructor

        public TopUpPerspective() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            _cam = Camera.main;
            FlipCamera();
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        void FlipCamera()
        {
            _cam.transform.rotation = Quaternion.Euler(60, 0, 180);
        }

        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            _cam.transform.rotation = Quaternion.Euler(60, 0, 0);
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }

        #endregion
    }
}
