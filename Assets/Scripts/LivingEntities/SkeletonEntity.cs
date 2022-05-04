using Abilities;
using AI.FSM;
using AI.FSM.Connections;
using AI.FSM.EnemyStates.Orc;
using AI.FSM.EnemyStates.Skeleton;
using Interfaces;
using Managers;
using PlayerScripts;

namespace LivingEntities
{
    public class SkeletonEntity : EnemyEntity, IMelee
    {
        private StateMachine<SkeletonEntity> _stateMachine;

        private StateSkeletonChase _chaseState;
        private StateSkeletonDeath _deathState;
        private StateSkeletonAttack _attackState;

        private StateConnectionOnDeath<SkeletonEntity> _connectionChaseDeath;
        private StateConnectionEntityInMeleeRange<SkeletonEntity> _connectionEntityInMeleeRange;
        private StateAttackConnectionOnFinish<SkeletonEntity> _connectionOnAttackFinish;

        public Melee Melee { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            Immunity |= Immunities.ImmuneToBleeding;

            Melee = new Melee(this);
        }

        protected override void Start()
        {
            base.Start();

            _steeringBehaviour.SeekOn();

            Player player = GameManager.Instance.Player;

            _chaseState = new StateSkeletonChase(this, player);
            _deathState = new StateSkeletonDeath(this);
            _attackState = new StateSkeletonAttack(this);

            _connectionChaseDeath = new StateConnectionOnDeath<SkeletonEntity>(_deathState, this);
            _chaseState.AddConnection(_connectionChaseDeath);

            _connectionEntityInMeleeRange =
                new StateConnectionEntityInMeleeRange<SkeletonEntity>(_attackState, this, player.transform);
            _chaseState.AddConnection(_connectionEntityInMeleeRange);

            _connectionOnAttackFinish = new StateAttackConnectionOnFinish<SkeletonEntity>(_chaseState, _attackState);
            _attackState.AddConnection(_connectionOnAttackFinish);

            _stateMachine = new StateMachine<SkeletonEntity>(this, _chaseState);

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