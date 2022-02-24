using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LivingEntities;

public class Projectile : MonoBehaviour
{
    //[SerializeField] private GameObject hitEffect;

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
