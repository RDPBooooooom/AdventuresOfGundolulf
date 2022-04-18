using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HalfTimeShow : StandardScroll
    {
        public HalfTimeShow() : base()
        {

        }

        protected override void ApplyEffect()
        {

            Debug.Log("Activated " + GetType().Name);
        }
    }
}
