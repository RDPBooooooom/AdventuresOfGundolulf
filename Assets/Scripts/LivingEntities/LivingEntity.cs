using System;
using System.Collections.Generic;
using AI.FSM;
using Items;
using UnityEngine;
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
        #endregion

        #region Properties

        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public float Health
        {
            get => _health; 
            protected set => _health = value;
        }

        public int Attack
        {
            get => _attack;
            protected set => _attack = value;  
        }

        public int Intelligence
        {
            get => _intelligence;
            protected set => _intelligence = value;
        }

        public float Range
        {
            get => _range;
            protected set => _range = value;
        }

        public float Haste
        {
            get => _haste;
            set => _haste = value;
        }

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public bool IsAlive
        {
            get => _isAlive;
            protected set => _isAlive = value;
        }

        public Transform MeleeAttackPoint
        {
            get => _meleeAttackPoint;
            protected set => _meleeAttackPoint = value;
        }

        public float MeleeAttackRange
        {
            get => _meleeAttackRange;
            protected set => _meleeAttackRange = value;
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

        #endregion

        #region Events

        public event OnDeathEventHandler OnDeathEvent;

        #endregion

        #region Unity Methods

        protected virtual void Awake()
        {
            OnDeathEvent += OnDeath;

            IsAlive = true;
            Health = _maxHealth;
            
            EquippedItems = new List<Item>();
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
            Health -= amount;
            if (Health <= 0)
            {
                OnDeathEvent?.Invoke();
            }
        }

        protected virtual void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Entity died [" + GetType().Name + "]");
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(MeleeAttackPoint.position, MeleeAttackRange);
        }

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

        public abstract void MoveEntity();

        public abstract void UpdateVelocity();

        public void ResetVelocity()
        {
            Velocity = Vector3.zero;
        }
        
    }
}