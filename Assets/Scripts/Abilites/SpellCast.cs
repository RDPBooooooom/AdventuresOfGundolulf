using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : Ability
{
    #region Fields

    private LivingEntity _targetEntity;

    #endregion

    public SpellCast(LivingEntity owner, LivingEntity targetEntity) : base(owner)
    {
        cooldown = 100 / owner.Haste;
        isReady = true;
        _targetEntity = targetEntity;
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
        _targetEntity.DamageEntity(_owner.Intelligence);
    }
}
