using System;
using UnityEngine;

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
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _hostileEntityLayers;

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

        public Transform AttackPoint
        {
            get => _attackPoint;
            protected set => _attackPoint = value;
        }

        public float AttackRange
        {
            get => _attackRange;
            protected set => _attackRange = value;
        }

        public LayerMask HostileEntityLayers
        {
            get => _hostileEntityLayers;
            protected set => _hostileEntityLayers = value;
        }

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
        
        }
    
        #endregion

        public void HealEntity(float amount)
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

        public void DamageEntity(float amount)
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
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
        }

    }
}