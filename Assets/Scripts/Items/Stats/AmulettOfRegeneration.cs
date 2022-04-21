using Levels.Rooms;
using LivingEntities;
using System;
using System.Collections;
using UnityEngine;
using Managers;
using Utils;

namespace Items.Stats
{
    public class AmulettOfRegeneration : StatsItem
    {
        #region Fields

        private MonoBehaviourDummy _dummy = MonoBehaviourDummy.Dummy;
        private Room _currentRoom = GameManager.Instance.LevelManager.CurrentRoom;

        private float _oldHealth;
        private int _regenerateValue = 1;
        private bool _inCombat;

        #endregion

        #region Constructor

        public AmulettOfRegeneration() : base()
        {
            Value = 15;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            _oldHealth = equipOn.MaxHealth;
            equipOn.MaxHealth /= 2;

            if (equipOn.Health > equipOn.MaxHealth)
            {
                equipOn.Health = equipOn.MaxHealth;
            }
            _inGameUI.UpdateHealthbar();

            _currentRoom.EnterRoom += OnEnterRoom;
            _currentRoom.RoomCleared += OnRoomCleared;
            _dummy.StartCoroutine(Regenerate(equipOn));
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.MaxHealth = _oldHealth;
            _inGameUI.UpdateHealthbar();

            _dummy.StopCoroutine(Regenerate(unequipFrom));
            _currentRoom.EnterRoom -= OnEnterRoom;
            _currentRoom.RoomCleared -= OnRoomCleared;
        }

        #endregion

        #region Helper Methods

        IEnumerator Regenerate(LivingEntity entityToHeal)
        {
            while (!_inCombat)
            {
                yield return new WaitForSeconds(1);
                entityToHeal.HealEntity(_regenerateValue);
                _inGameUI.UpdateHealthbar();
            }
        }

        private void OnEnterRoom(Room entering)
        {
            if (entering is CombatRoom)
                _inCombat = true;
        }

        private void OnRoomCleared()
        {
            _inCombat = false;
        }

        #endregion
    }
}
