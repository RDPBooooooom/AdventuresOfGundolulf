using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LivingEntities;

public class Projectile : MonoBehaviour
{
    #region Fields

    private Vector3 _ownerPosition;
    private float _rangeDivisor = 20;

    //[SerializeField] private GameObject hitEffect;

    #endregion

    #region Properties

    public SpellCast Owner { get; set; }

    #endregion

    #region Delegates

    public delegate void ProjectileEndHandler(Projectile projectile);

    #endregion

    #region Unity Methods

    void Start()
    {
        _ownerPosition = Owner.GetPosition();
        IgnoreCollision();
    }

    private void Update()
    {
        DestroyProjectile();
    }

    #endregion

    #region Helper Methods
    private void DestroyProjectile()
    {
        float distance = Vector3.Distance(_ownerPosition, transform.position);

        if (distance >= Owner.GetRange() / _rangeDivisor)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Collision

    private void IgnoreCollision()
    {
        foreach (Collider characterCollider in Owner.GetCollider())
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), characterCollider);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInLayerMask(other.gameObject.layer, Owner.GetHostileEntites()))
        {
            if (other.GetComponent<LivingEntity>() != null)
            {
                Owner.DealDamage(other.GetComponent<LivingEntity>());
            }
        }
    }
    
    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }

    #endregion
}
