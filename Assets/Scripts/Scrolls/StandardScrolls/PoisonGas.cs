using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class PoisonGas : StandardScroll
    {
        public PoisonGas() : base()
        {
            
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
