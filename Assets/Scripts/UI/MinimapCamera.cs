using UnityEngine;
using PlayerScripts;

namespace UserInterface
{
    public class MinimapCamera : MonoBehaviour
    {
        #region Fields

        private Player _player;

        #endregion

        #region Unity Methods

        // Start is called before the first frame update
        void Start()
        {
            _player = FindObjectOfType<PlayerScripts.Player>();

        }
        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = new Vector3(_player.transform.position.x, 20, _player.transform.position.z);
        }

        #endregion
    }
}
