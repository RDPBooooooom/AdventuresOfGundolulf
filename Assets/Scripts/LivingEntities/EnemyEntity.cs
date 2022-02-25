using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LivingEntities
{
    public class EnemyEntity : LivingEntity
    {
        Melee melee;
        PlayerScripts.Player player;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            melee = new Melee(this);
            player = FindObjectOfType<PlayerScripts.Player>();
        }

        protected virtual void FixedUpdate()
        {
            if (PlayerInRange())
                melee.Use();
            else
                FollowPlayer();
        }

        protected virtual void FollowPlayer()
        {
            //ToDo
            Debug.Log("Following player");
        }

        protected virtual bool PlayerInRange()
        {
            Debug.Log(Vector3.Distance(transform.position, player.transform.position) <= MeleeAttackRange);
            return Vector3.Distance(transform.position, player.transform.position) <= MeleeAttackRange;
        }
    }
}
