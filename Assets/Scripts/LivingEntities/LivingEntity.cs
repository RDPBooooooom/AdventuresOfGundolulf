using System;
using System.Collections.Generic;
using Items;
using PlayerScripts;
using UnityEngine;
using Managers;
using UI;

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

        private Animator _animator;
        private InGameUI _inGameUI;

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
            set => _attack = value;  
        }

        public int Intelligence
        {
            get => _intelligence;
            set => _intelligence = value;
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

        public Animator Animator
        {
            get => _animator;
            protected set => _animator = value;
        }

        private List<Item> EquippedItems {  get; set; }

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

        private void Start()
        {
            _inGameUI = GameManager.Instance.UIManager.MainCanvas.GetComponent<InGameUI>();
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
            _animator.SetTrigger(AnimatorStrings.GetHitString);

            Health -= amount;
            if (Health <= 0)
            {
                OnDeathEvent?.Invoke();
            }
        }

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
    }
}