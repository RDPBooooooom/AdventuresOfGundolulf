using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HelloDarkness : StandardScroll
    {
        public HelloDarkness()
        {
            Cost = 3;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
