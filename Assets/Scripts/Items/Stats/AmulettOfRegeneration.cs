using Levels.Rooms;
using LivingEntities;
using System;
using System.Collections;
using UnityEngine;

namespace Items.Stats
{
    public class AmulettOfRegeneration : StatsItem
    {
        float oldHealth;
        int regenerateValue = 1;
        Utils.MonoBehaviourDummy dummy = Utils.MonoBehaviourDummy.Dummy;
        Room currentRoom = Managers.GameManager.Instance.LevelManager.CurrentRoom;
        bool inCombat;

        public AmulettOfRegeneration() : base()
        {
            Value = 15;
        }
        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
            oldHealth = _player.MaxHealth;
            _player.MaxHealth /= 2;
            currentRoom.EnterRoom += OnEnterRoom;
            currentRoom.RoomCleared += OnRoomCleared;
            dummy.StartCoroutine(Regenerate());
        }


        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            _player.MaxHealth = oldHealth;
            dummy.StopCoroutine(Regenerate());
            currentRoom.EnterRoom -= OnEnterRoom;
            currentRoom.RoomCleared -= OnRoomCleared;
        }


        IEnumerator Regenerate()
        {
            while (!inCombat)
            {
                yield return new WaitForSeconds(1);
                _player.HealEntity(regenerateValue);
            }
        }

        private void OnEnterRoom(Room entering)
        {
            if (entering is CombatRoom)
                inCombat = true;
        }

        private void OnRoomCleared()
        {
            inCombat = false;
        }

    }
}
