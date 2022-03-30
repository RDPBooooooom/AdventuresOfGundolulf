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

            _shopkeeper = Instantiate(_shopkeeperPrefab, transform.position, Quaternion.identity, transform);
        }
    }
}