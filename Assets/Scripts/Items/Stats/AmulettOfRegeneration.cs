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
            oldHealth = equipOn.MaxHealth;
            equipOn.MaxHealth /= 2;
            currentRoom.EnterRoom += OnEnterRoom;
            currentRoom.RoomCleared += OnRoomCleared;
            dummy.StartCoroutine(Regenerate(equipOn));
        }


        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
            unequipFrom.MaxHealth = oldHealth;
            dummy.StopCoroutine(Regenerate(unequipFrom));
            currentRoom.EnterRoom -= OnEnterRoom;
            currentRoom.RoomCleared -= OnRoomCleared;
        }


        IEnumerator Regenerate(LivingEntity entityToHeal)
        {
            while (!inCombat)
            {
                yield return new WaitForSeconds(1);
                entityToHeal.HealEntity(regenerateValue);
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
