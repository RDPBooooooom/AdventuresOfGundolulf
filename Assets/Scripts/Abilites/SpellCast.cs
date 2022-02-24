using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Ability
{
    #region Fields

    private GameObject _projectile;

    #endregion

    public SpellCast(LivingEntity owner) : base(owner)
    {
        cooldown = 100 / owner.Haste;
        isReady = true;
    }

    public override void Use()
    {
        if (isReady)
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

    public LayerMask HostileEntites() => _owner.HostileEntityLayers;
}
