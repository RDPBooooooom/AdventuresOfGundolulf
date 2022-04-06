﻿using System.Runtime.Serialization;
using Items;
using Managers;

namespace Levels.Rooms
{
    public class TreasureRoom : Room
    {
        public override void Enter()
        {
            base.Enter();

            if (WasVisited) return;
            
            OnRoomCleared();
            
            DroppedItem item = GameManager.Instance.ItemManager.GetRandomDroppedItem();
            item.transform.position = this.transform.position;
        }
    }
}