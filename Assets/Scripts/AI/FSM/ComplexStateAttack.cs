namespace AI.FSM
{
    public abstract class ComplexStateAttack<T> : ComplexState<T>
    {
        public bool IsFinished { get; protected set; }
        
        protected ComplexStateAttack(T owner) : base(owner)
        {
            IsFinished = false;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            IsFinished = false;
        }
    }
}