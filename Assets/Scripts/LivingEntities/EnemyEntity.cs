using Items;
using Managers;
using UnityEngine;
using Random = System.Random;

namespace LivingEntities
{
    public abstract class EnemyEntity : LivingEntity
    {
        #region Declaring Variables

        private GameObject _coin;

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _coin = Resources.Load<GameObject>("Prefabs/Objects/Coin");
        }
        
        #endregion

        #region Methods

        protected virtual void Drop()
        {
            int amount = 1;
            Random random = new Random();
            int chance = random.Next(1, 100);
            if (chance >= 50)
            {
                amount = 5;
            }
            else if (chance <= 40)
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

        #region Movement

        public override void UpdateVelocity()
        {
            Vector3 steeringForce = _steeringBehaviour.Calculate(Target.transform.position);

            Vector3 accel = steeringForce / Mass; // F = m * a => a = F / m. [a] = m/s^2

            Velocity += accel;
            
            Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
            
            _animator.SetFloat(Animator.StringToHash("MoveX"), Velocity.x, 0.1f, Time.deltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), Velocity.z, 0.1f, Time.deltaTime);
            
            Debug.DrawLine(transform.position, transform.position + Velocity, Color.magenta, Time.deltaTime);
        }
       
        public override void MoveEntity()
        {
            transform.Translate(Velocity * Time.deltaTime, Space.World);
            //_rigidbody.AddForce(Velocity, ForceMode.VelocityChange);
            transform.rotation = Quaternion.LookRotation(Velocity, Vector3.up);
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
