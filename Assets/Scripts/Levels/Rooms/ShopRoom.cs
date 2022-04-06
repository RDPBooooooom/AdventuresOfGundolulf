using UnityEngine;

namespace Levels.Rooms
{
    public class ShopRoom : Room
    {
        [SerializeField] private GameObject _shopkeeperPrefab;
        private GameObject _shopkeeper;


        protected override void Start()
        {
            base.Start();

            Transform transform = this.transform;
            _shopkeeper = Instantiate(_shopkeeperPrefab, transform.position, Quaternion.identity, transform);
        }
        
        public override void Enter()
        {
            base.Enter();

            if (WasVisited) return;
            
            OnRoomCleared();
        }
    }
}