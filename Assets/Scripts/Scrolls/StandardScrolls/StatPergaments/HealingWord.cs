using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HealingWord : StandardScroll
    {
        #region Constructor

        public HealingWord() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            Managers.GameManager.Instance.Player.HealEntity(50);
        }

        #endregion
    }
}
