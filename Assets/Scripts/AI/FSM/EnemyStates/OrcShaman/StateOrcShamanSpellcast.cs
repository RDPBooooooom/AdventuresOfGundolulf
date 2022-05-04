using LivingEntities;
using UnityEngine;

namespace AI.FSM.EnemyStates.OrcShaman
{
    public class StateOrcShamanSpellcast : ComplexStateAttack<OrcShamanEntity>
    {
        public StateOrcShamanSpellcast(OrcShamanEntity owner) : base(owner)
        {
            _owner.SpellCast.OnFinish += OnFinish;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.SpellCast.Use();
        }

        private void OnFinish()
        {
            IsFinished = true;
        }
    }
}