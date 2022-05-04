using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HomedoorCompanion : StandardScroll
    {
        #region Fields

        private GameObject _door = Resources.Load<GameObject>("Prefabs/Objects/Door");

        #endregion

        #region Constructor

        public HomedoorCompanion() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            SpawnDoor();
        }

        void SpawnDoor()
        {
            Object.Instantiate(_door, Managers.GameManager.Instance.LevelManager.CurrentRoom.transform.position, Quaternion.identity);
        }

        #endregion
    }
}
