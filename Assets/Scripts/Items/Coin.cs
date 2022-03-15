using PlayerScripts;
using UnityEngine;

namespace Items
{
    public class Coin : MonoBehaviour
    {
        private Player _player;

        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<Player>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _player.Gold += 1;
                Destroy(gameObject);
            }

        }

    }
}
