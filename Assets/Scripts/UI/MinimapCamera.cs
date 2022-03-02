using UnityEngine;

namespace UserInterface
{
    public class MinimapCamera : MonoBehaviour
    {
        PlayerScripts.Player player;

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<PlayerScripts.Player>();

        }
        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = new Vector3(player.transform.position.x, 20, player.transform.position.z);
        }
    }
}
