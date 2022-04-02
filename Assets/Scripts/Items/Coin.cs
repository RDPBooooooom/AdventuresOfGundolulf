using PlayerScripts;
using UnityEngine;

namespace Items
{
    public class Coin : MonoBehaviour
    {
        private Player _player = Managers.GameManager.Instance.Player;

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
