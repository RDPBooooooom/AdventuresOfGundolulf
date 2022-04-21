using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HalfTimeShow : StandardScroll
    {
        #region Constructor

        public HalfTimeShow() : base()
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
