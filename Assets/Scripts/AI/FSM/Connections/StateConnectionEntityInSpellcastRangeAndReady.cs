using LivingEntities;
using UnityEngine;

namespace AI.FSM.Connections
{
    public class StateConnectionEntityInSpellcastRangeAndReady<T> : StateConnection<T> where T : LivingEntity
    {
        private T _owner;
        private Transform _target;
        private SpellCast _spell;
        private Transform _spellCastPoint;

        public StateConnectionEntityInSpellcastRangeAndReady(State<T> connectedState, T owner, Transform target,
            SpellCast spell) : base(connectedState)
        {
            _owner = owner;
            _target = target;
            _spell = spell;
            _spellCastPoint = owner.SpellCastAttackPoint;
        }

        public override bool IsConditionFulFilled()
        {
            return _spell.IsReady && (Vector3.Distance(_spellCastPoint.position, _target.position) - 5f <= _owner.Range);
        }
    }
}