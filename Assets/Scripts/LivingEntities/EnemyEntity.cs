using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

namespace LivingEntities
{
    public abstract class EnemyEntity : LivingEntity
    {
        protected Melee melee;
        protected Player player;

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

        protected virtual void FollowPlayer()
        {
            //ToDo
           // Debug.Log("Following player");
        }

        protected virtual bool PlayerInRange()
        {
            //Debug.Log(Vector3.Distance(transform.position, player.transform.position) <= MeleeAttackRange);
            return Vector3.Distance(transform.position, player.transform.position) <= MeleeAttackRange;
        }
    }
}
