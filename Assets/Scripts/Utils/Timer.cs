using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Timer
    {

        private MonoBehaviour _owner;

        public float Time { get; set; }
        public bool IsReady { get; private set; }

        public Timer(MonoBehaviour owner, float time)
        {
            _owner = owner;
            Time = time;
            IsReady = true;
        }

        public void Start()
        {
            _owner.StartCoroutine(CooldownTimer());
        }

        protected IEnumerator CooldownTimer()
        {
            IsReady = false;

            yield return new WaitForSeconds(Time);

            IsReady = true;
        }
    }
}