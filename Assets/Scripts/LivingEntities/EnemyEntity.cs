using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using Random = System.Random;

namespace LivingEntities
{
    public abstract class EnemyEntity : LivingEntity
    {
        #region Declaring Variables
        protected Melee melee;
        protected Player player;
        [SerializeField] GameObject coin;

        [Header("Item Prefabs")]
        List<GameObject> items = new List<GameObject>();

        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        protected void Start()
        {
            melee = new Melee(this);
            player = FindObjectOfType<Player>();
            items.Add(Resources.Load<GameObject>("Items/AmulettofRegeneration"));
            items.Add(Resources.Load<GameObject>("Items/HealthPotion"));
            //items.Add(Resources.Load<GameObject>("Items/HourGlass"));
            items.Add(Resources.Load<GameObject>("Items/MagicMilk"));
            //items.Add(Resources.Load<GameObject>("Items/MagicSplit"));
            items.Add(Resources.Load<GameObject>("Items/PickAxe"));
            //items.Add(Resources.Load<GameObject>("Items/ReverseEye"));
            items.Add(Resources.Load<GameObject>("Items/RingOfResting"));
            items.Add(Resources.Load<GameObject>("Items/Shield"));
            items.Add(Resources.Load<GameObject>("Items/Staff"));
            //items.Add(Resources.Load<GameObject>("Items/Swoop"));
            items.Add(Resources.Load<GameObject>("Items/Sword"));
            //items.Add(Resources.Load<GameObject>("Items/ToxicPaper"));
        }

        protected void FixedUpdate()
        {
            if (PlayerInRange())
                melee.Use();
            else
                FollowPlayer();
        }
        #endregion

        #region Methods
        protected virtual void FollowPlayer()
        {
            //ToDo
           // Debug.Log("Following player");
        }

        protected virtual bool PlayerInRange()
        {
            //Debug.Log(Vector3.Distance(transform.position, player.transform.position));// <= MeleeAttackRange);
            return Vector3.Distance(transform.position, player.transform.position) <= MeleeAttackRange;
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
                if(chance <= 10)
                {
                    amount = 5;
                    if(chance <= 3)
                    {
                        amount = 0;
                        DropItem();
                    }
                }
            }
            if(amount != 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    Instantiate(coin, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
                }
            }
        }

        protected virtual void DropItem()
        {
            Random random = new Random();
            int item = random.Next(0, 8);
            Instantiate(items[item], new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
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
