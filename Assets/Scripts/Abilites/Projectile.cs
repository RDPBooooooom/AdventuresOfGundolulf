using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LivingEntities;

public class Projectile : MonoBehaviour
{
    public SpellCast Owner { get; set; }

    //[SerializeField] private GameObject hitEffect;

    private void Awake()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer, Owner.HostileEntites()))
        {
            Debug.Log("Hit an Enemy!!!!!!");
            if (other.GetComponent<LivingEntity>() != null)
            {
                Debug.Log("Dealt Damage");
                Owner.DealDamage(other.GetComponent<LivingEntity>());
            }
        }
    }
    
    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask & (1 << layer));
    }
}
