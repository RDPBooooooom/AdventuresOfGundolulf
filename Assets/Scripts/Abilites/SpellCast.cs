using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Ability
{
    #region Fields

    private GameObject _projectile;

    #endregion

    #region Methods

    public SpellCast(LivingEntity owner) : base(owner)
    {
        Cooldown = 100 / owner.Haste;
    }

    public override void Use()
    {
        if (IsReady)
        {
            Projectile();
            StartCooldown();
        }
    }

    public void Projectile()
    {
        _projectile = Object.Instantiate(_owner.ProjectilePrefab, _owner.SpellCastAttackPoint.position, _owner.SpellCastAttackPoint.rotation);
        Projectile projectile = _projectile.GetComponent<Projectile>();
        projectile.Owner = this;

        Rigidbody rigidbody = _projectile.GetComponent<Rigidbody>();
        rigidbody.AddForce(_owner.SpellCastAttackPoint.forward * _owner.ProjectileForce, ForceMode.Impulse);
    }

    public void DealDamage(LivingEntity targetEntity)
    {
        targetEntity.DamageEntity(_owner.Intelligence);
    }

    #endregion

    #region Get Values

    public LayerMask GetHostileEntites() => _owner.HostileEntityLayers;

    public float GetRange() => _owner.Range;

    public Vector3 GetPosition() => _owner.transform.position;

    public Collider[] GetCollider() => _owner.GetComponents<Collider>();

    #endregion
}
