using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class Warrior : StandardScroll
    {
        #region Constructor

        public Warrior() : base()
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
