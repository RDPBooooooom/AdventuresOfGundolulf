using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class SkipIt : StandardScroll
    {
        #region Constructor

        public SkipIt() : base()
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
