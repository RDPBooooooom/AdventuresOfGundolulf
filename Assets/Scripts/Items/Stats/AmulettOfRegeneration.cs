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
        private float oldHealth;
        private int regenerateValue = 1;
        private MonoBehaviourDummy dummy = MonoBehaviourDummy.Dummy;
        private Room currentRoom = GameManager.Instance.LevelManager.CurrentRoom;
        private bool inCombat;

        public AmulettOfRegeneration() : base()
        {
            Value = 15;
        }

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);

            oldHealth = equipOn.MaxHealth;
            equipOn.MaxHealth /= 2;

            if (equipOn.Health > equipOn.MaxHealth)
            {
                equipOn.Health = equipOn.MaxHealth;
            }
            inGameUI.UpdateHealthbar();

            currentRoom.EnterRoom += OnEnterRoom;
            currentRoom.RoomCleared += OnRoomCleared;
            dummy.StartCoroutine(Regenerate(equipOn));
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);

            unequipFrom.MaxHealth = oldHealth;
            inGameUI.UpdateHealthbar();

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
                inGameUI.UpdateHealthbar();
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
