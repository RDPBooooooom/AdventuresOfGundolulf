using System;
using System.Collections.Generic;
using AI.FSM;
using Items;
using UnityEngine;
using Managers;
using PlayerScripts;
using UI;
using Utils;

namespace LivingEntities
{
    public abstract class LivingEntity : MonoBehaviour
    {
        #region Fields

        [Header("Entity Stats")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        [SerializeField] private int _attack;
        [SerializeField] private int _intelligence;
        [SerializeField] private float _range;
        [SerializeField] private float _haste;
        [SerializeField] private float _speed;
        [SerializeField] private bool _isAlive;

        [Header("Melee")]
        [SerializeField] private Transform _meleeAttackPoint;
        [SerializeField] private float _meleeAttackRange;
        [SerializeField] private LayerMask _hostileEntityLayers;

        [Header("SpellCast")]
        [SerializeField] private Transform _spellCastAttackPoint;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _projectileForce;
        
        [Header("Obstacle Avoidance")]
        [SerializeField] private LayerMask _obstacleAvoidanceMask;
        protected SteeringBehaviour _steeringBehaviour;

        protected Animator _animator;
        protected Immunities _immunity;
        private bool _stopActions;

        #endregion

        #region Properties

        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                OnUpdateMaxHealthEvent?.Invoke();
            }
        }

        public float Health
        {
            get => _health;
            set
            {
                _health = value;
                OnUpdateHealthEvent?.Invoke();
            }
        }

        public int Attack
        {
            get => _attack;
            set
            {
                _attack = value;
                OnUpdateAttackEvent?.Invoke();
            }
        }

        public int Intelligence
        {
            get => _intelligence;
            set
            {
                _intelligence = value;
                OnUpdateIntelligenceEvent?.Invoke();
            }
        }

        public float Range
        {
            get => _range;
            set
            {
                _range = value;
                OnUpdateRangeEvent?.Invoke();
            }
        }

        public float Haste
        {
            get => _haste;
            set
            {
                _haste = value;
                OnUpdateHasteEvent?.Invoke();
            }
        }

        public float Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                OnUpdateSpeedEvent?.Invoke();
            }
        }

        public bool IsAlive
        {
            get => _isAlive;
            set => _isAlive = value;
        }

        public Transform MeleeAttackPoint
        {
            get => _meleeAttackPoint;
            set => _meleeAttackPoint = value;
        }

        public float MeleeAttackRange
        {
            get => _meleeAttackRange;
            set => _meleeAttackRange = value;
        }

        public LayerMask HostileEntityLayers
        {
            get => _hostileEntityLayers;
            protected set => _hostileEntityLayers = value;
        }

        public Transform SpellCastAttackPoint
        {
            get => _spellCastAttackPoint;
            protected set => _spellCastAttackPoint = value;
        }

        public GameObject ProjectilePrefab
        {
            get => _projectilePrefab;
            protected set => _projectilePrefab = value;
        }

        public float ProjectileForce
        {
            get => _projectileForce;
            protected set => _projectileForce = value;
        }

        public LayerMask CollisionDetectionMask
        {
            get => _obstacleAvoidanceMask;
            private set => _obstacleAvoidanceMask = value;
        }
        public Animator Animator
        {
            get => _animator;
            protected set => _animator = value;
        }

        public Immunities Immunity
        {
            get => _immunity;
            set => _immunity = value;
        }

        public bool StopActions
        {
            get => _stopActions;
            set => _stopActions = value;
        }

        private List<Item> EquippedItems {  get; set; }
        public LivingEntity Target { get; set; }
        
        
        
        public Vector3 Velocity { get; protected set; }
        public Vector3 HeadingDirection { get; protected set; }

        // Sollten nur wenn nötig geändert werden
        public float MaxSpeed => Speed;
        public float Mass { get; protected set; }
        public float MaxForce { get; set; }
        public float MaxTurnRate { get; set; }

        #endregion

        #region Delegates

        public delegate void OnDeathEventHandler();
        
        public delegate void StatUpdateHandler();
        
        #endregion

        #region Events

        public event StatUpdateHandler OnUpdateMaxHealthEvent;
        public event StatUpdateHandler OnUpdateHealthEvent;
        public event StatUpdateHandler OnUpdateAttackEvent;
        public event StatUpdateHandler OnUpdateIntelligenceEvent;
        public event StatUpdateHandler OnUpdateRangeEvent;
        public event StatUpdateHandler OnUpdateHasteEvent;
        public event StatUpdateHandler OnUpdateSpeedEvent;
        public event OnDeathEventHandler OnDeathEvent;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            OnDeathEvent += OnDeath;

            IsAlive = true;
            Health = _maxHealth;
            
            EquippedItems = new List<Item>();
            _animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            _steeringBehaviour = new SteeringBehaviour(this);
            
            Velocity = Vector3.zero;
            MaxForce = Speed * GameConstants.SpeedMultiplier;
            MaxTurnRate = 10f;
            Mass = 1f;
        }

        #endregion

        #region Damage Methods
        public virtual void HealEntity(float amount)
        {
            if (IsAlive)
            {
                Health += amount;

                if (Health >= MaxHealth)
                {
                    Health = MaxHealth;
                }
            }
        }

        public virtual void DamageEntity(float amount)
        {
            _animator.SetTrigger(AnimatorStrings.GetHitString);

            Health -= amount;
            if (this == GameManager.Instance.Player)
                GameManager.Instance.AudioManager.PlayOuchSound();
            else
                GameManager.Instance.AudioManager.PlayOuchOrcSound();
            if (Health <= 0)
            {
                OnDeathEvent?.Invoke();
            }
        }

        #endregion

        #region Death Methods

        protected virtual void OnDeath()
        {
            IsAlive = false;
            Animator.SetTrigger(AnimatorStrings.DeathString);

            // Disabing all colliders if the entity died to prevent blocking the player while death animation
            foreach (Collider collider in GetComponents<Collider>())
            {
                collider.enabled = false;
            }
        }

        /// <summary>
        /// Gets called after the death animation is finished
        /// </summary>
        private void DestroyOnDeath()
        {
            if (this is Player)
            {
                GameManager.Instance.UIManager.MainCanvas.GetComponent<InGameUI>().IngamePanel.SetActive(false);
                GameManager.Instance.UIManager.MainCanvas.GetComponent<InGameUI>().DeathPanel.SetActive(true);

                Time.timeScale = 0;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(MeleeAttackPoint.position, MeleeAttackRange);
        }

        public void UpdateStats()
        {
            OnUpdateMaxHealthEvent?.Invoke();
            OnUpdateHealthEvent?.Invoke();
            OnUpdateAttackEvent?.Invoke();
            OnUpdateIntelligenceEvent?.Invoke();
            OnUpdateRangeEvent?.Invoke();
            OnUpdateHasteEvent?.Invoke();
            OnUpdateSpeedEvent?.Invoke();
        }

        #region Equip Methods

        public void Equip(Item item)
        {
            EquippedItems.Add(item);
            item.Equip(this);
        }

        public void Unequip(Item item)
        {
            EquippedItems.Remove(item);
            item.Unequip(this);
        }
        #endregion

        #region Movement

        public abstract void MoveEntity();

        public abstract void UpdateVelocity();

        public void ResetVelocity()
        {
            Velocity = Vector3.zero;
        }

        #endregion
        
        [Flags]
        public enum Immunities
        {
            ImmuneToBleeding = 1,           // 0001
            ImmuneToPoison = 2,             // 0010
            ImmuneToPetrification = 4,      // 0100
            ImmuneToMelee = 8               // 1000
        }
    }
}