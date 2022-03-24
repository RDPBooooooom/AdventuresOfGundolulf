using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Ability
{
    #region Methods

    public Melee(LivingEntity owner) : base(owner)
    {
        Cooldown = 100 / owner.Haste;
    }

    public override void Use()
    {
        if (IsReady)
        {
            DoMeele();
            StartCooldown();
        }
    }

    public void DoMeele()
    {
        _owner.Animator.SetTrigger(AnimatorStrings.MeleeString);

        Collider[] hostileEntitiesHit = Physics.OverlapSphere(_owner.MeleeAttackPoint.position, _owner.MeleeAttackRange, _owner.HostileEntityLayers);

        foreach (Collider hostileEntity in hostileEntitiesHit)
        {
            LivingEntity _targetEntity = hostileEntity.GetComponent<LivingEntity>();
            _targetEntity.DamageEntity(_owner.Attack);
        }
    }

    #endregion
}
