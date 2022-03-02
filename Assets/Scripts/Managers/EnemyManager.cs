using System.Collections.Generic;
using LivingEntities;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<LivingEntity> _enemies;


        public List<LivingEntity> GetRandomEnemies()
        {
            //TODO Get Random enemies based on current stage etc
            return _enemies;
        }



    }
}