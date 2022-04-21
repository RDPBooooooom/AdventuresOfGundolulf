using LivingEntities;
using UnityEngine;

namespace AI.FSM.Connections
{
    public class StateConnectionEntityInRange<T> : StateConnection<T> where T : LivingEntity
    {

        private T _owner;
        
        private Transform _target;
        private Transform _attackPoint;

        public StateConnectionEntityInRange(State<T> connectedState, T owner, Transform target) : base(connectedState)
        {
            _owner = owner;
            _target = target;
            _attackPoint = _owner.MeleeAttackPoint;
        }

        public override bool IsConditionFulFilled()
        {
            return Vector3.Distance(_attackPoint.position, _target.position) - 0.5f <= _owner.MeleeAttackRange;
        }
    }
}