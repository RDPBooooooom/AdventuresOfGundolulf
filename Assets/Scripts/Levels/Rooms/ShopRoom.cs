using UnityEngine;

namespace Levels.Rooms
{
    public class ShopRoom : Room
    {
        #region Fields

        [SerializeField] private GameObject _shopkeeperPrefab;
        private GameObject _shopkeeper;

        #endregion

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            Transform transform = this.transform;
            _shopkeeper = Instantiate(_shopkeeperPrefab, transform.position, Quaternion.identity, transform);
        }

        #endregion

        #region Room Methods

        public override void Enter()
        {
            base.Enter();

            if (WasVisited) return;
            
            OnRoomCleared();
        }

        #endregion
    }
}