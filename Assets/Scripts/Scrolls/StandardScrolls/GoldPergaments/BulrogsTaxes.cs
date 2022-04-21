using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class BulrogsTaxes : StandardScroll
    {
        #region Constructor

        public BulrogsTaxes() : base()
        {
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            CalculateNewGold();
        }

        void CalculateNewGold()
        {
            Managers.GameManager.Instance.Player.Gold = Mathf.CeilToInt(Managers.GameManager.Instance.Player.Gold /2);
        }

        #endregion
    }
}
