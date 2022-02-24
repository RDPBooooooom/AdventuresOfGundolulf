using System;
using System.Collections.Generic;
using LivingEntities;

namespace Levels.Rooms
{
    public class CombatRoom : Room
    {
        #region Fields

        private List<LivingEntity> _enemies;

        #endregion

        #region Properties

        public Boolean IsLeavable { get; set; }

        #endregion

        #region Unity Methods

        #endregion

        #region Room Methods

        public override void Enter()
        {
            base.Enter();
            //TODO Spawn enemies
        }

        public override void Leave()
        {
            if(IsLeavable) base.Leave();
        }

        #endregion
    }
}