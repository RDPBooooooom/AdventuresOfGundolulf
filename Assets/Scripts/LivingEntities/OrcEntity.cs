﻿using UnityEngine;

namespace LivingEntities
{
    public class OrcEntity : LivingEntity
    {
        Melee melee;
        Player.Player player;

        private void Start()
        {
            melee = new Melee(this);
            player = FindObjectOfType<Player.Player>(); 
        }
        private void FixedUpdate()
        {
            if (PlayerInRange())
                melee.Use();
            else
                FollowPlayer();
            
        }


        void FollowPlayer()
        {
            //ToDo
            Debug.Log("Following player");
        }

        bool PlayerInRange()
        {
            return Vector3.Distance(transform.position, player.transform.position) <= MeleeAttackRange;
        }
    }
}