using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class Warrior : StandardScroll
    {
        public Warrior() : base()
        {

        }

        protected override void ApplyEffect()
        {
            
            Debug.Log("Activated " + GetType().Name);
        }
    }
}
