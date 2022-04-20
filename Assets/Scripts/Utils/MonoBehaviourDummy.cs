using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
    public class MonoBehaviourDummy : MonoBehaviour
    {
        public static MonoBehaviourDummy Dummy { get; private set; }

        private void Awake()
        {
            Dummy = this;
        }
    }
}