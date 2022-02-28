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
        #endregion

        #region Unity Methods
        // Start is called before the first frame update
        protected void Start()
        {
            melee = new Melee(this);
            player = FindObjectOfType<Player>();
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
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
            }
        }

        protected virtual void DropItem()
        {
            //TODO: get random Item and spawn
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
