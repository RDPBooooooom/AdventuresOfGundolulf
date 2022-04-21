using AI.FSM.Connections;
using LivingEntities;

namespace AI.FSM.EnemyStates.Orc
{
    public class StateOrcAttack : ComplexStateAttack<OrcEntity>
    {
        public StateOrcAttack(OrcEntity owner) : base(owner)
        {
            owner.Melee.OnFinish += OnFinish;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _owner.Melee.Use();
        }

        private void OnFinish()
        {
            IsFinished = true;
        }
    }
}