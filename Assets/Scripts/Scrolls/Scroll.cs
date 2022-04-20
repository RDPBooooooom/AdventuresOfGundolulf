using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Scrolls
{
    public abstract class Scroll
    {
        #region Properties

        public string DisplayName { get; protected set; }

        public int Cost { get; protected set; }

        public string Description { get; protected set; }

        #endregion

        #region Delegates

        public delegate void ScrollActivateEventHandler(Scroll scroll);

        #endregion

        #region Events

        public event ScrollActivateEventHandler ActivateEvent;

        #endregion

        public Scroll()
        {
            string name = GetType().Name;

            DisplayName = ScrollLoader.GetDisplayName(name);
            Description = ScrollLoader.GetDescription(name);
            Cost = ScrollLoader.GetCost(name);
        }

        public void Activate()
        {
            ActivateEvent?.Invoke(this);
            ApplyEffect();
        }

        protected abstract void ApplyEffect();

    }
}
