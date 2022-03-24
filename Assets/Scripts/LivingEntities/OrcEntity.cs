using System;
using AI.FSM;
using AI.FSM.EnemyStates.Orc;
using Levels.Rooms;
using Managers;
using UnityEngine;

namespace LivingEntities
{
    public class OrcEntity : EnemyEntity
    {
        #region States

        private StateMachine<OrcEntity> _stateMachine;
        
        private StateOrcChase _chaseState;

        #endregion
        
        protected override void Start()
        {
            base.Start();
            
            _steeringBehaviour.SeekOn();

            _chaseState = new StateOrcChase(this, GameManager.Instance.Player);

            _stateMachine = new StateMachine<OrcEntity>(this, _chaseState);
            
            _chaseState.InitState(_stateMachine);
            
            _stateMachine.StartStateMachine();
        }

        protected void Update()
        {
            _stateMachine.OnUpdate();
        }
    }
}