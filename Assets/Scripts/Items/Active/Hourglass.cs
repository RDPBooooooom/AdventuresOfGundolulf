using Assets.Scripts.Interfaces;
using Levels.Rooms;
using LivingEntities;
using Managers;
using System.Collections;
using UnityEngine;
using Utils;

namespace Items.Active
{
    public class Hourglass : ActiveItem, IUsable
    {
        #region Fields

        private CombatRoom _currentRoom;

        #endregion

        #region Constructor

        public Hourglass() : base()
        {
            Value = 15;
            Cooldown = 30;
        }

        #endregion

        #region Equip

        public override void Equip(LivingEntity equipOn)
        {
            base.Equip(equipOn);
        }

        public override void Unequip(LivingEntity unequipFrom)
        {
            base.Unequip(unequipFrom);
        }

        #endregion


        #region Effect

        protected override void Effect()
        {
            if (GameManager.Instance.LevelManager.CurrentRoom is CombatRoom)
            {
                _currentRoom = (CombatRoom)GameManager.Instance.LevelManager.CurrentRoom;
                MonoBehaviourDummy.Dummy.StartCoroutine(SlowTime());
            }
            else if (GameManager.Instance.LevelManager.CurrentRoom is BossRoom)
            {
                //_currentRoom = (BossRoom)GameManager.Instance.LevelManager.CurrentRoom;
                MonoBehaviourDummy.Dummy.StartCoroutine(SlowTime());
            }
            base.Effect();
        }

        IEnumerator SlowTime()
        {
            Debug.Log("Enemies slowed");
            foreach (LivingEntity enemy in _currentRoom.Enemies)
            {
                enemy.Haste -= 50;
                enemy.Speed -= 5;
            }

            yield return new WaitForSeconds(5);

            Debug.Log("Enemies not slowed anymore");
            foreach (LivingEntity enemy in _currentRoom.Enemies)
            {
                enemy.Haste += 50;
                enemy.Speed += 5;
            }
        }

        #endregion
    }
}