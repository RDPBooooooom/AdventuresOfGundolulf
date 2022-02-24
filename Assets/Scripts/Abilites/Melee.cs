using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability
{
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
        }
    }

    public void DoMeele()
    {
<<<<<<< HEAD
        Collider[] hostileEntitiesHit = Physics.OverlapSphere(_owner.MeleeAttackPoint.position, _owner.MeleeAttackRange, _owner.HostileEntityLayers);

        foreach (Collider hostileEntity in hostileEntitiesHit)
        {
            LivingEntity _targetEntity = hostileEntity.GetComponent<LivingEntity>();
=======
        Collider[] hitHostileEntity = Physics.OverlapSphere(_owner.AttackPoint.position, _owner.AttackRange, _owner.HostileEntityLayers);
        Debug.Log(hitHostileEntity.Length);
        foreach (Collider hostileEntity in hitHostileEntity)
        {
            _targetEntity = hostileEntity.GetComponentInChildren<LivingEntity>();
            Debug.Log(_targetEntity.name);
>>>>>>> 4f56d21269cfbee4e7571d38dbd9797a14a2c9f0
            _targetEntity.DamageEntity(_owner.Attack);
        }

        StartCooldown();
    }
}
