using Items;
using Managers;
using UnityEngine;
using Random = System.Random;

namespace LivingEntities
{
    public abstract class EnemyEntity : LivingEntity
    {
        #region Declaring Variables

        [SerializeField] private GameObject _coin;

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
        }
        
        #endregion

        #region Methods

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

        #region Movement

        public override void UpdateVelocity()
        {
            Vector3 steeringForce = _steeringBehaviour.Calculate(Target.transform.position);

            _animator.SetFloat(Animator.StringToHash("MoveX"), steeringForce.x, 0.1f, Time.deltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), steeringForce.z, 0.1f, Time.deltaTime);

            Vector3 accel = steeringForce / Mass; // F = m * a => a = F / m. [a] = m/s^2

            Velocity += accel;
            
            Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
            
            Debug.DrawLine(transform.position, transform.position + Velocity, Color.magenta, Time.deltaTime);
        }
       
        public override void MoveEntity()
        {
            transform.Translate(Velocity * Time.deltaTime, Space.World);
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
