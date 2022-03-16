using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{

    public class TeleporterMalfunction : StandardScroll
    {
        public TeleporterMalfunction()
        {
            Cost = 1;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
        }
    }

}
