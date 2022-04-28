using LivingEntities;
using Utils;
using Interfaces;
using System.Collections;
using UnityEngine;

namespace Items.Teleport
{
    public class Swoop : TeleportItem
    {
        #region Fields

        private Timer _channelingTimer;
        private ITeleporter _teleporter;
        private int _oldTeleportRange;

        #endregion

        #region Constructor

        public Swoop() : base()
        {
            Value = 10;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            _channelingTimer = new Timer(equipOn, 0.5f);

            if (!(equipOn.GetType() == typeof(ISpellcaster))) return;
            _teleporter = (ITeleporter)equipOn;
            _oldTeleportRange = _teleporter.Teleport.TeleportRange;
            _teleporter.Teleport.TeleportRange = 50;
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            _teleporter.Teleport.TeleportRange = _oldTeleportRange;
        }

        #endregion
    }
}
