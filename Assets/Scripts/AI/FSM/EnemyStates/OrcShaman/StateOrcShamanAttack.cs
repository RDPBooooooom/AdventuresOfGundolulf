using LivingEntities;

namespace AI.FSM.EnemyStates.OrcShaman
{
    public class StateOrcShamanAttack : ComplexStateAttack<OrcShamanEntity>
    {
        public StateOrcShamanAttack(OrcShamanEntity owner) : base(owner)
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