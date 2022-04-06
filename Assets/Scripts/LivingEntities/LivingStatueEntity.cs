using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LivingEntities
{
    public class LivingStatueEntity : EnemyEntity
    {
        protected override void Awake()
        {
            base.Awake();
            Immunity = Immunities.ImmuneToPetrification | Immunities.ImmuneToMelee;
        }
    }
}
