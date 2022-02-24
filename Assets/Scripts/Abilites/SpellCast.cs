using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Ability
{
    #region Fields

    private LivingEntity _targetEntity;
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
            DoSpellCast();
        }
    }

    public void DoSpellCast()
    {
        Projectile();

        Collider[] hostileEntitiesHit = Physics.OverlapSphere(_projectile.transform.position, _projectile.transform.localScale.x, _owner.HostileEntityLayers);

        foreach (Collider hostileEntity in hostileEntitiesHit)
        {
            LivingEntity _targetEntity = hostileEntity.GetComponent<LivingEntity>();
            _targetEntity.DamageEntity(_owner.Intelligence);
        }
    }

    public void Projectile()
    {
        _projectile = Object.Instantiate(_owner.ProjectilePrefab, _owner.SpellCastAttackPoint.position, _owner.SpellCastAttackPoint.rotation);
        Rigidbody rigidbody = _projectile.GetComponent<Rigidbody>();
        rigidbody.AddForce(_owner.SpellCastAttackPoint.forward * _owner.ProjectileForce, ForceMode.Impulse);
    }
}
