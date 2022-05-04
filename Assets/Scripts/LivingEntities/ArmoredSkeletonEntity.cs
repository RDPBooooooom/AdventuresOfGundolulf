using System.Collections;
using System.Collections.Generic;
using Abilities;
using AI.FSM;
using AI.FSM.Connections;
using AI.FSM.EnemyStates.ArmoredSkeleton;
using AI.FSM.EnemyStates.Orc;
using Effects;
using Interfaces;
using Managers;
using PlayerScripts;
using UnityEngine;

namespace LivingEntities
{
    public class ArmoredSkeletonEntity : EnemyEntity, IMelee, IBlocker
    {
        #region Fields

        [Header("Blocking")] 
        [SerializeField] private float _blockDuration = 2;

        [SerializeField] private float _blockSwitchTickRate = 1;
        [SerializeField] private float _blockSwitchChance = 0.35f;

        private StateMachine<ArmoredSkeletonEntity> _stateMachine;

        private StateArmoredSkeletonChase _chaseState;
        private StateArmoredSkeletonDeath _deathState;
        private StateArmoredSkeletonAttack _attackState;
        private StateArmoredSkeletonBlock _blockState;

        private StateConnectionOnDeath<ArmoredSkeletonEntity> _connectionChaseDeath;
        private StateConnectionEntityInMeleeRange<ArmoredSkeletonEntity> _connectionEntityInMeleeRange;
        private StateAttackConnectionOnFinish<ArmoredSkeletonEntity> _connectionOnAttackFinish;
        private StateAttackConnectionOnFinish<ArmoredSkeletonEntity> _connectionOnBlockFinish;


        private CombinedConnection<ArmoredSkeletonEntity> _connectionBlockReadyRandom;
        private StateConnectionAbilityReady<ArmoredSkeletonEntity> _connectionOnBlockReady;
        private StateConnectionRandomSwitch<ArmoredSkeletonEntity> _connectionRandomBlock;

        #endregion

        public Melee Melee { get; protected set; }

        public Block Block { get; protected set; }

        protected override void Awake()
        {
            base.Awake();

            Melee = new Melee(this);
            Block = new Block(this, _blockDuration);
        }

        protected override void Start()
        {
            base.Start();

            _steeringBehaviour.SeekOn();

            Player player = GameManager.Instance.Player;

            _chaseState = new StateArmoredSkeletonChase(this, player);
            _deathState = new StateArmoredSkeletonDeath(this);
            _attackState = new StateArmoredSkeletonAttack(this);
            _blockState = new StateArmoredSkeletonBlock(this);

            _connectionChaseDeath = new StateConnectionOnDeath<ArmoredSkeletonEntity>(_deathState, this);
            _chaseState.AddConnection(_connectionChaseDeath);

            _connectionEntityInMeleeRange =
                new StateConnectionEntityInMeleeRange<ArmoredSkeletonEntity>(_attackState, this, player.transform);
            _chaseState.AddConnection(_connectionEntityInMeleeRange);

            _connectionOnAttackFinish =
                new StateAttackConnectionOnFinish<ArmoredSkeletonEntity>(_chaseState, _attackState);
            _attackState.AddConnection(_connectionOnAttackFinish);

            _connectionRandomBlock = new StateConnectionRandomSwitch<ArmoredSkeletonEntity>(_blockState, _blockSwitchTickRate, _blockSwitchChance);
            _connectionOnBlockReady = new StateConnectionAbilityReady<ArmoredSkeletonEntity>(_blockState, Block);
            _connectionBlockReadyRandom = new CombinedConnection<ArmoredSkeletonEntity>(_blockState);
            _connectionBlockReadyRandom.AddConnection(_connectionRandomBlock);
            _connectionBlockReadyRandom.AddConnection(_connectionOnBlockReady);
            _chaseState.AddConnection(_connectionBlockReadyRandom);

            _connectionOnBlockFinish =
                new StateAttackConnectionOnFinish<ArmoredSkeletonEntity>(_chaseState, _blockState);
            _blockState.AddConnection(_connectionOnBlockFinish);

            _stateMachine = new StateMachine<ArmoredSkeletonEntity>(this, _chaseState);

            _chaseState.InitState(_stateMachine);
            _deathState.InitState(_stateMachine);
            _attackState.InitState(_stateMachine);
            _blockState.InitState(_stateMachine);

            _stateMachine.StartStateMachine();
        }

        protected void Update()
        {
            _stateMachine.OnUpdate();
        }
    }
}