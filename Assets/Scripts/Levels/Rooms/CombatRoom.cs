using System;
using System.Collections.Generic;
using System.Linq;
using LivingEntities;
using Managers;
using UnityEngine;

namespace Levels.Rooms
{
    public class CombatRoom : Room
    {
        #region Fields

        private List<LivingEntity> _enemiesPrefabs;
        private List<LivingEntity> _enemies;

        #endregion

        #region Properties

        public bool IsLeavable { get; set; }

        #endregion

        #region Room Methods

        private void CheckCleared()
        {
            if (IsLeavable) return;
            if (_enemies.Any(entity => entity.IsAlive))
            {
                IsLeavable = false;
                return;
            }

            IsLeavable = true;
            OnRoomCleared();
        }

        public override void Enter()
        {
            base.Enter();
            //TODO Spawn enemies
            if (WasVisited) return;
            LoadEnemies();
            _enemies = new List<LivingEntity>();
            foreach (LivingEntity entity in _enemiesPrefabs)
            {
                LivingEntity activeEnemy = Instantiate(entity.gameObject, transform.position, Quaternion.identity)
                    .GetComponent<LivingEntity>();
                activeEnemy.OnDeathEvent += CheckCleared;
                _enemies.Add(activeEnemy);
            }
        }

        private void LoadEnemies()
        {
            _enemiesPrefabs = GameManager.Instance.EnemyManager.GetRandomEnemies();
        }

        public override bool CanLeave()
        {
            if (!IsLeavable) return false;

            return true;
        }

        #endregion
    }
}