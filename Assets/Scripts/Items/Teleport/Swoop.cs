using LivingEntities;
using Utils;

namespace Items.Teleport
{
    public class Swoop : TeleportItem
    {
        private Timer _channelingTimer;

        public Swoop() : base()
        {
            Value = 10;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            _channelingTimer = new Timer(equipOn, 0.5f);
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
        }

    }
}
