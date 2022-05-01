using UnityEngine;
//using https://www.youtube.com/watch?v=iGAdaZ1ICaI&t=249s

public class Fogger : MonoBehaviour
{
    PlayerScripts.Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Managers.GameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, 8, player.transform.position.z - 3.3f);
        
    }
}
