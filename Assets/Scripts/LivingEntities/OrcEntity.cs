using System;
using Abilities;
using AI.FSM;
using AI.FSM.Connections;
using AI.FSM.EnemyStates;
using AI.FSM.EnemyStates.Orc;
using Levels.Rooms;
using Managers;
using PlayerScripts;
using UnityEngine;

namespace LivingEntities
{
    public class OrcEntity : EnemyEntity
    {

        #region Abilities

        private Melee _melee;

        #endregion
        
        #region States

        private StateMachine<OrcEntity> _stateMachine;
        
        private StateOrcChase _chaseState;
        private StateOrcDeath _deathState;
        private StateOrcAttack _attackState;

        private StateConnectionOnDeath<OrcEntity> _connectionChaseDeath;
        private StateConnectionEntityInMeleeRange<OrcEntity> _connectionEntityInMeleeRange;
        private StateAttackConnectionOnFinish<OrcEntity> _connectionOnAttackFinish;

        public Melee Melee
        {
            get => _melee;
            private set => _melee = value;
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            Melee = new Melee(this);
        }

        protected override void Start()
        {
            base.Start();
            
            _steeringBehaviour.SeekOn();

            Player player = GameManager.Instance.Player;

            _chaseState = new StateOrcChase(this, player);
            _deathState = new StateOrcDeath(this);
            _attackState = new StateOrcAttack(this);

            _connectionChaseDeath = new StateConnectionOnDeath<OrcEntity>(_deathState,this);
            _chaseState.AddConnection(_connectionChaseDeath);

            _connectionEntityInMeleeRange = new StateConnectionEntityInMeleeRange<OrcEntity>(_attackState, this, player.transform);
            _chaseState.AddConnection(_connectionEntityInMeleeRange);

            _connectionOnAttackFinish = new StateAttackConnectionOnFinish<OrcEntity>(_chaseState, _attackState);
            _attackState.AddConnection(_connectionOnAttackFinish);
            
            _stateMachine = new StateMachine<OrcEntity>(this, _chaseState);
            
            _chaseState.InitState(_stateMachine);
            _deathState.InitState(_stateMachine);
            _attackState.InitState(_stateMachine);
            
            _stateMachine.StartStateMachine();
        }

        protected void Update()
        {
            _stateMachine.OnUpdate();
        }
    }
}