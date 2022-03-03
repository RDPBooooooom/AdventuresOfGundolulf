using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace LivingEntities
{
    public abstract class EnemyEntity : LivingEntity
    {
        #region Declaring Variables

        protected Melee _melee;
        protected Player _player;
        protected Animator _animator;
        protected Rigidbody _rigidbody;
        [SerializeField] GameObject _coin;

        [Header("Item Prefabs")]
        List<GameObject> _items = new List<GameObject>();

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        protected void Start()
        {
            _melee = new Melee(this);
            _player = FindObjectOfType<Player>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            _items.Add(Resources.Load<GameObject>("Items/AmulettofRegeneration"));
            _items.Add(Resources.Load<GameObject>("Items/HealthPotion"));
            //items.Add(Resources.Load<GameObject>("Items/HourGlass"));
            _items.Add(Resources.Load<GameObject>("Items/MagicMilk"));
            //items.Add(Resources.Load<GameObject>("Items/MagicSplit"));
            _items.Add(Resources.Load<GameObject>("Items/PickAxe"));
            //items.Add(Resources.Load<GameObject>("Items/ReverseEye"));
            _items.Add(Resources.Load<GameObject>("Items/RingOfResting"));
            _items.Add(Resources.Load<GameObject>("Items/Shield"));
            _items.Add(Resources.Load<GameObject>("Items/Staff"));
            //items.Add(Resources.Load<GameObject>("Items/Swoop"));
            _items.Add(Resources.Load<GameObject>("Items/Sword"));
            //items.Add(Resources.Load<GameObject>("Items/ToxicPaper"));
        }

        protected void FixedUpdate()
        {            
            if (PlayerInRange())
                _melee.Use();
            else
                FollowPlayer();
        }
        #endregion

        #region Methods
        protected virtual void FollowPlayer()
        {
            Vector2 targetVector = new Vector2(_player.transform.position.x - transform.position.x, _player.transform.position.z - transform.position.z).normalized;

            _animator.SetFloat(Animator.StringToHash("MoveX"), targetVector.x, 0.1f, Time.fixedDeltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), targetVector.x, 0.1f, Time.fixedDeltaTime);

            Vector3 direction = new Vector3(targetVector.x * Speed, 0, targetVector.y * Speed);

            transform.LookAt(_player.transform.position);
            _rigidbody.AddForce(direction * (Time.fixedDeltaTime * GameConstants.SpeedMultiplier), ForceMode.VelocityChange);
        }

        protected virtual bool PlayerInRange()
        {
            //Debug.Log(Vector3.Distance(transform.position, player.transform.position));// <= MeleeAttackRange);
            return Vector3.Distance(transform.position, _player.transform.position) <= MeleeAttackRange;
        }

        protected virtual void Drop()
        {
            Debug.Log("Dropping");
            int amount = 0;
            Random random = new Random();
            int chance = random.Next(1, 100);

            if (chance <= 50)
            {
                amount = 1;
            }
            else if (chance <= 60)
            {
                amount = 5;
            }
            else if (chance <= 63)
            {
                DropItem();
            }

            if(amount != 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(_coin, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                }
            }
        }

        protected virtual void DropItem()
        {
            Random random = new Random();
            int item = random.Next(0, 8);
            Instantiate(_items[item], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
        }

        #endregion

        #region Override Methods
        protected override void OnDeath()
        {
            Drop();
            base.OnDeath();
        }
        #endregion
    }
}
