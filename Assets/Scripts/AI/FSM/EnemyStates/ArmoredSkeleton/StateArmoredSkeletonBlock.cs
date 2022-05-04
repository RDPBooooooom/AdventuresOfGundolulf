using LivingEntities;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace AI.FSM.EnemyStates.ArmoredSkeleton
{
    public class StateArmoredSkeletonBlock : ComplexStateAttack<ArmoredSkeletonEntity>
    {
        public StateArmoredSkeletonBlock(ArmoredSkeletonEntity owner) : base(owner)
        {
            _owner.Block.OnFinish += OnFinish;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.Block.Use();
        }

        private void OnFinish()
        {
            IsFinished = true;
        }
    }
}