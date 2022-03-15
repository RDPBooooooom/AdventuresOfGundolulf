using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class BroughtASwordToAMagicFight : StandardScroll
    {
        public BroughtASwordToAMagicFight()
        {
            Cost = 1;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
