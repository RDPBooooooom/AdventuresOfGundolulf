using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HelloDarkness : StandardScroll
    {
        public HelloDarkness() : base()
        {
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
