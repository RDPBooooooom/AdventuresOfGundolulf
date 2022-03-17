using Assets.Scripts;
using Managers;
using UnityEngine;

namespace Items
{
    public class DroppedItem : MonoBehaviour, IInteractable
    {
        #region Properties

        public Item Item { get; set; }

        #endregion

        public void Interact()
        {
            GameManager.Instance.Player.Equip(Item);
            Destroy(gameObject);
        }
    }
}

