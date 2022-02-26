using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability
{
    #region Methods

    public Melee(LivingEntity owner) : base(owner)
    {
        cooldown = 100 / owner.Haste;
        isReady = true;
    }

    public override void Use()
    {
        if (isReady)
        {
            DoMeele();
            StartCooldown();
        }
    }

    public void DoMeele()
    {
        Collider[] hostileEntitiesHit = Physics.OverlapSphere(_owner.MeleeAttackPoint.position, _owner.MeleeAttackRange, _owner.HostileEntityLayers);

        foreach (Collider hostileEntity in hostileEntitiesHit)
        {
            LivingEntity _targetEntity = hostileEntity.GetComponent<LivingEntity>();
            _targetEntity.DamageEntity(_owner.Attack);
            Debug.Log(_owner.name + " hit " + _targetEntity.name);
        }
    }

    #endregion
}
