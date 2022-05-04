using System.Collections;
using System.Collections.Generic;
using Abilities;
using AI.FSM;
using AI.FSM.Connections;
using AI.FSM.EnemyStates.OrcShaman;
using Effects;
using Interfaces;
using Managers;
using PlayerScripts;
using UnityEngine;

namespace LivingEntities
{
    public class OrcShamanEntity : EnemyEntity, IMelee, ISpellcaster
    {
        private StateMachine<OrcShamanEntity> _stateMachine;

        private StateOrcShamanChase _chaseState;
        private StateOrcShamanDeath _deathState;
        private StateOrcShamanAttack _attackState;
        private StateOrcShamanSpellcast _spellCastState;

        private StateConnectionOnDeath<OrcShamanEntity> _connectionChaseDeath;
        private StateConnectionEntityInMeleeRange<OrcShamanEntity> _connectionEntityInMeleeRange;
        private StateAttackConnectionOnFinish<OrcShamanEntity> _connectionOnAttackFinish;
        private StateAttackConnectionOnFinish<OrcShamanEntity> _connectionOnSpellFinish;

        private CombinedConnection<OrcShamanEntity> _connectionReadyAndInSpellRange;
        private StateConnectionAbilityReady<OrcShamanEntity> _connectionSpellcastReady;
        private StateConnectionEntityInSpellcastRange<OrcShamanEntity> _connectionEntitySpellcast;

        public Melee Melee { get; protected set; }
        public SpellCast SpellCast { get; protected set; }

        [Header("Poisen Effect on Spell")] [SerializeField]
        private float _poisenChance = 0.35f;

        [SerializeField] private float _poisenDuration = 2;

        protected override void Awake()
        {
            base.Awake();

            Melee = new Melee(this);
            SpellCast = new SpellCast(this);
            SpellCast.AddEffect(new Poison(_poisenChance, _poisenDuration));
        }

        protected override void Start()
        {
            base.Start();

            _steeringBehaviour.SeekOn();

            Player player = GameManager.Instance.Player;

            _chaseState = new StateOrcShamanChase(this, player);
            _deathState = new StateOrcShamanDeath(this);
            _attackState = new StateOrcShamanAttack(this);
            _spellCastState = new StateOrcShamanSpellcast(this);

            _connectionChaseDeath = new StateConnectionOnDeath<OrcShamanEntity>(_deathState, this);
            _chaseState.AddConnection(_connectionChaseDeath);

            _connectionEntityInMeleeRange =
                new StateConnectionEntityInMeleeRange<OrcShamanEntity>(_attackState, this, player.transform);
            _chaseState.AddConnection(_connectionEntityInMeleeRange);

            _connectionOnAttackFinish = new StateAttackConnectionOnFinish<OrcShamanEntity>(_chaseState, _attackState);
            _attackState.AddConnection(_connectionOnAttackFinish);

            _connectionOnSpellFinish = new StateAttackConnectionOnFinish<OrcShamanEntity>(_chaseState, _spellCastState);
            _spellCastState.AddConnection(_connectionOnSpellFinish);

            _connectionEntitySpellcast =
                new StateConnectionEntityInSpellcastRange<OrcShamanEntity>(_spellCastState, this, player.transform);
            _connectionSpellcastReady = new StateConnectionAbilityReady<OrcShamanEntity>(_spellCastState, SpellCast);

            _connectionReadyAndInSpellRange = new CombinedConnection<OrcShamanEntity>(_spellCastState);
            _connectionReadyAndInSpellRange.AddConnection(_connectionEntitySpellcast);
            _connectionReadyAndInSpellRange.AddConnection(_connectionSpellcastReady);
            _chaseState.AddConnection(_connectionReadyAndInSpellRange);

            _stateMachine = new StateMachine<OrcShamanEntity>(this, _chaseState);

            _chaseState.InitState(_stateMachine);
            _deathState.InitState(_stateMachine);
            _attackState.InitState(_stateMachine);
            _spellCastState.InitState(_stateMachine);

            _stateMachine.StartStateMachine();
        }

        protected void Update()
        {
            _stateMachine.OnUpdate();
        }
    }
}