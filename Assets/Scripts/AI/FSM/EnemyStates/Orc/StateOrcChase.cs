using LivingEntities;
using UnityEngine;

namespace AI.FSM.EnemyStates.Orc
{
    public class StateOrcChase : State<OrcEntity>
    {

        private LivingEntity _toChase;
        
        public StateOrcChase(OrcEntity owner, LivingEntity toChase) : base(owner)
        {
            _toChase = toChase;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.Target = _toChase;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            
            _owner.UpdateVelocity();
            _owner.MoveEntity();
        }

        public override void OnExit()
        {
            base.OnExit();
            
            _owner.ResetVelocity();
        }
    }
}