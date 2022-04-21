using Assets.Scripts.Interfaces;

namespace Items.Active
{
    public class Hourglass : ActiveItem, IUsable
    {
        #region Constructor

        public Hourglass() : base()
        {
            Value = 15;
            Cooldown = 30;
        }

        #endregion

        #region Effect

        protected override void Effect()
        {
            // Do effect

            _cooldown.Start();
        }

        #endregion
    }
}