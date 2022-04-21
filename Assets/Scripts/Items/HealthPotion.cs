using System.Collections;
using System.Collections.Generic;
using Items;
using LivingEntities;
using UnityEngine;

public class HealthPotion : Item
{
    #region Fields

    private int _healAmount = 50;

    #endregion

    #region Constructor

    public HealthPotion() : base()
    {
        Value = 10; 
    }

    #endregion

    #region Equip

    public override void Equip(LivingEntity equipOn)
    {
        base.Equip(equipOn);
        _player.HealEntity(_healAmount);
        Unequip(equipOn);
    }

    #endregion
}
