using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability
{
    #region Fields

    private LivingEntity _targetEntity;

    #endregion

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
        Collider[] hitHostileEntity = Physics.OverlapSphere(_owner.AttackPoint.position, _owner.AttackRange, _owner.HostileEntityLayers);

        foreach (Collider hostileEntity in hitHostileEntity)
        {
            _targetEntity = hostileEntity.GetComponent<LivingEntity>();
            _targetEntity.DamageEntity(_owner.Attack);
        }

        StartCooldown();
    }
}
