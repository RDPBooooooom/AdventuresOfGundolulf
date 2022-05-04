using System.Collections;
using UnityEngine;

namespace Utils
{
    public class Timer
    {
        #region Fields

        private MonoBehaviour _owner;

        #endregion

        #region Properties

        public float Time { get; set; }
        public bool IsReady { get; private set; }

        #endregion

        #region Delegates

        public delegate void TimerReadyHandler();

        #endregion

        #region events

        public event TimerReadyHandler OnTimerReady;

        #endregion
        
        #region Constructor

        public Timer(MonoBehaviour owner, float time)
        {
            _owner = owner;
            Time = time;
            IsReady = true;
        }

        #endregion

        #region Timer

        public void Start()
        {
            _owner.StartCoroutine(CooldownTimer());
        }

        protected IEnumerator CooldownTimer()
        {
            IsReady = false;

            yield return new WaitForSeconds(Time);

            IsReady = true;
            OnTimerReady?.Invoke();
        }

        #endregion
    }
}