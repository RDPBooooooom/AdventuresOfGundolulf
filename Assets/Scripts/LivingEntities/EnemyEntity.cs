using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AI.FSM;
using AI.FSM.EnemyStates.Orc;
using Items;
using Managers;
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
        protected Animator _animator;

        [SerializeField] GameObject _coin;

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _melee = new Melee(this);
            _animator = GetComponent<Animator>();
        }

        protected void FixedUpdate()
        {            
           // if (PlayerInRange())
             //   _melee.Use();
        
           // else
                //FollowPlayer();
        }
        #endregion

        #region Methods
       /* protected virtual void FollowPlayer()
        {
            Vector2 targetVector = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.z - transform.position.z).normalized;

            _animator.SetFloat(Animator.StringToHash("MoveX"), targetVector.x, 0.1f, Time.fixedDeltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), targetVector.x, 0.1f, Time.fixedDeltaTime);

            Vector3 direction = new Vector3(targetVector.x * Speed, 0, targetVector.y * Speed);

            transform.LookAt(Target.transform.position);
            _rigidbody.AddForce(direction * (Time.fixedDeltaTime * GameConstants.SpeedMultiplier), ForceMode.VelocityChange);
        }*/

       public override void UpdateVelocity()
       {
           Vector3 steeringForce = _steeringBehaviour.Calculate(Target.transform.position);

           Vector3 accel = steeringForce / Mass; // F = m * a => a = F / m. [a] = m/s^2

           Velocity += accel;

           Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
       }
       
       public override void MoveEntity()
       {
           transform.Translate(Velocity * Time.deltaTime, Space.World);
           transform.rotation = Quaternion.LookRotation(Velocity);
       }

       protected virtual bool PlayerInRange()
        {
            //Debug.Log(Vector3.Distance(transform.position, player.transform.position));// <= MeleeAttackRange);
            return Vector3.Distance(transform.position, Target.transform.position) <= MeleeAttackRange;
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
            else if (chance <= 100) //63
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
            DroppedItem item = GameManager.Instance.ItemManager.GetRandomDroppedItem();

            if (item == null) return;

            Vector3 position = transform.position;
            item.transform.position = new Vector3(position.x, position.y + 0.5f, position.z);
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
