using System.Collections.Generic;
using Levels.Rooms;
using LivingEntities;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<LivingEntity> _enemies;
        [SerializeField] private List<LivingEntity> _bossEnemies;

        #endregion

        #region Get Enemies

        public List<LivingEntity> GetRandomEnemies()
        {
            //TODO Get Random enemies based on current stage etc
            return _enemies;
        }
        
        public List<LivingEntity> GetRandomBoss()
        {
            //TODO Get Random enemies based on current stage etc
            return _bossEnemies;
        }

        #endregion
    }
}