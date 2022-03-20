using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class NoHitChallenge : StandardScroll
    {
        int currentGoldAmount;
        int removedGold;
        float playerStartingHP;
        

        public NoHitChallenge()
        {
            Cost = 0;
        }
        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            currentGoldAmount = Managers.GameManager.Instance.Player.Gold;
            playerStartingHP = Managers.GameManager.Instance.Player.Health;
            if (currentGoldAmount >= 20)
                removedGold = 20;
            else
                removedGold = currentGoldAmount;
            Managers.GameManager.Instance.Player.Gold -= removedGold;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom += OnLeavingRoom;
        }
        private void OnLeavingRoom(Levels.Rooms.Room leaving, Levels.Rooms.Room toEnter)
        {
            if(playerStartingHP == Managers.GameManager.Instance.Player.Health)
                Managers.GameManager.Instance.Player.Gold += removedGold * 2;
            Managers.GameManager.Instance.LevelManager.CurrentRoom.LeaveRoom -= OnLeavingRoom;
        }
    }
}
