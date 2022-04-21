using LivingEntities;
using Utils;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace Items.Teleport
{
    public class Swoop : TeleportItem
    {
        private Timer _channelingTimer;
        private ITeleporter _teleporter;
        private int _oldTeleportRange; 

        public Swoop() : base()
        {
            Value = 10;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            _channelingTimer = new Timer(equipOn, 0.5f);
            _teleporter = (ITeleporter)equipOn;
            _oldTeleportRange = _teleporter.Teleport.TeleportRange;
            _teleporter.Teleport.TeleportRange = 50;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _teleporter.Teleport.TeleportRange = _oldTeleportRange;
        }
    }
}
