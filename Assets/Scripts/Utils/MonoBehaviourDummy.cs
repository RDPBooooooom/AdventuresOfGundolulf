using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{

    public class MonoBehaviourDummy : MonoBehaviour
    {
        public static MonoBehaviourDummy dummy;
        Camera cam;

        private void Start()
        {
            dummy = this;
        }
    }
}
