using UnityEngine;
using Utils;

namespace AI.FSM.Connections
{
    public class StateConnectionRandomSwitch<T> : StateConnection<T>
    {

        private float _randomChance;
        private Timer _timer;

        public StateConnectionRandomSwitch(State<T> connectedState, float tickRate, float randomChance) : base(connectedState)
        {
            _timer = new Timer(MonoBehaviourDummy.Dummy, tickRate);
            _randomChance = randomChance;
        }

        public override bool IsConditionFulFilled()
        {
            if (!_timer.IsReady) return false;
            
            _timer.Start();
            return Random.value <= _randomChance;
        }
    }
}