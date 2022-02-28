using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UserInterface.InGameUI.Instance.Gold += 1;
            UserInterface.InGameUI.Instance.GoldAmount.text = UserInterface.InGameUI.Instance.Gold.ToString();
            Destroy(gameObject);
        }

    }

}
