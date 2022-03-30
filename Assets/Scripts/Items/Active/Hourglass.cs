using Assets.Scripts.Interfaces;

namespace Items.Active
{
    public class Hourglass : ActiveItem, IUsable
    {
        public Hourglass() : base()
        {
            Value = 15;
            Cooldown = 30;
        }

        protected override void Effect()
        {

            
            _cooldown.Start();
        }
    }
}