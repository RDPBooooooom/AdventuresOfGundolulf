using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            UserInterface.InGameUI.Instance.GoldAmount.text = _player.Gold.ToString();
            Destroy(gameObject);
        }

    }

}
