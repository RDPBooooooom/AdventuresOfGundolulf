using Abilities;
using LivingEntities;
using UnityEngine;

namespace AI.FSM.Connections
{
    public class StateConnectionEntityInSpellcastRange<T> : StateConnection<T> where T : LivingEntity
    {
        private T _owner;
        private Transform _target;
        private Transform _spellCastPoint;

        public StateConnectionEntityInSpellcastRange(State<T> connectedState, T owner, Transform target) : base(connectedState)
        {
            _owner = owner;
            _target = target;
            _spellCastPoint = owner.SpellCastAttackPoint;
        }

        public override bool IsConditionFulFilled()
        {
            return Vector3.Distance(_spellCastPoint.position, _target.position) - 5f <= _owner.Range;
        }
    }
}