using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class PoisonGas : StandardScroll
    {
        public PoisonGas()
        {
            Cost = 3;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
