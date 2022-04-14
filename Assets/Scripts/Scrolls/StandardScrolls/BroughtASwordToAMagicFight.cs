using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class BroughtASwordToAMagicFight : StandardScroll
    {
        public BroughtASwordToAMagicFight() : base()
        {
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
