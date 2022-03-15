namespace Scrolls
{
    public abstract class Scroll
    {
        #region Properties

        //TODO Likly needs to be a Scriptable Object
        public int Cost { get; protected set; }

        #endregion

        #region Delegates

        public delegate void ScrollActivateEventHandler(Scroll scroll);

        #endregion

        #region Events

        public event ScrollActivateEventHandler ActivateEvent;

        #endregion

        public void Activate()
        {
            ActivateEvent?.Invoke(this);
            ApplyEffect();
        }

        protected abstract void ApplyEffect();

    }
}
