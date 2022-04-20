using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class SkipIt : StandardScroll
    {
        public SkipIt() : base()
        {

        }

        protected override void ApplyEffect()
        {

            Debug.Log("Activated " + GetType().Name);
        }
    }
}
