using System.Collections;
using System.Collections.Generic;
using Items;
using LivingEntities;
using UnityEngine;

public class HealthPotion : Item
{
    int healAmount = 50;
    public HealthPotion() : base()
    {
        Value = 10; 
    }
    public override void Equip(LivingEntity equipOn)
    {
        base.Equip(equipOn);
        _player.HealEntity(healAmount);
        Unequip(equipOn);
    }
}
