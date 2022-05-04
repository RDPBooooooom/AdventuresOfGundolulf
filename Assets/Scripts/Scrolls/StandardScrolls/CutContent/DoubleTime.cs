using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class DoubleTime : StandardScroll
    {
        #region Constructor

        public DoubleTime() : base()
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
