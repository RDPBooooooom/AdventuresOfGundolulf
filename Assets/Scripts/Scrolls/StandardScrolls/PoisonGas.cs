using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class PoisonGas : StandardScroll
    {
        #region Constructor

        public PoisonGas() : base()
        {            
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }

        #endregion
    }
}
