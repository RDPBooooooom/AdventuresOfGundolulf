using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class Spinning : StandardScroll
    {
        Camera cam;
        public Spinning()
        {
            Cost = 3;
        }

        private void Start()
        {
            cam = Camera.main;
        }

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            SpinCamera();
        }

        void SpinCamera()
        {
            cam.transform.rotation = Quaternion.Euler(0, 0, cam.transform.rotation.z + 1);
        }
    }
}
