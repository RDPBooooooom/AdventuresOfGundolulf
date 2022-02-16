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

        public Boolean IsAlive
        {
            get => _isAlive;
            protected set => _isAlive = value;
        }

        #endregion

        #region Delegates

        public delegate void OnDeathEventHandler();

        #endregion

        #region Events

        public event OnDeathEventHandler OnDeathEvent;

        #endregion

        #region Unity Methods

        private void Awake()
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
            }
        }

        public void DamageEntity(float amount)
        {
            Health -= amount;

            if (Health <= 0)
            {
                OnDeathEvent.Invoke();
            }
        }

        protected virtual void OnDeath()
        {
            IsAlive = false;
            Debug.Log("Entity died [" + GetType().Name + "]");
        }
    
    }
}