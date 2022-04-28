using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;
using Managers;

public class Melee : Ability
{
    public delegate void MeleeDelegate(LivingEntity target, int damage);

    public event MeleeDelegate OnDamage;

    #region Constructor

    public Melee(LivingEntity owner) : base(owner)
    {
        Cooldown = 100 / owner.Haste;
    }

    #endregion

    #region Methods

    public override void Use()
    {
        if (IsReady)
        {
            DoMeele();
            GameManager.Instance.AudioManager.PlayAttackSound();
            StartCooldown();
        }
        OnAbilityFinshed();
    }

    public void DoMeele()
    {
        _owner.Animator.SetTrigger(AnimatorStrings.MeleeString);

        Collider[] hostileEntitiesHit = Physics.OverlapSphere(_owner.MeleeAttackPoint.position, _owner.MeleeAttackRange, _owner.HostileEntityLayers);

        foreach (Collider hostileEntity in hostileEntitiesHit)
        {
            LivingEntity _targetEntity = hostileEntity.GetComponent<LivingEntity>();
            _targetEntity.DamageEntity(_owner.Attack);

            if (_targetEntity is LivingStatueEntity)
            {
                OnDamage?.Invoke(_targetEntity, _owner.Attack);
            }

            foreach (Effect effect in _effects)
            {
                effect.TryApplyEffect(_targetEntity);
            }
        }
    }

    #endregion
}
