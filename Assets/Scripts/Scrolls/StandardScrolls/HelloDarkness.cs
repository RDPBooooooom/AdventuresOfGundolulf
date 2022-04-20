using UnityEngine;
using Levels.Rooms;
using Utils;

namespace Scrolls.StandardScrolls
{
    public class HelloDarkness : StandardScroll
    {
        GameObject fog = Resources.Load<GameObject>("Prefabs/Fog");
        GameObject fogger;
        Room currentRoom;
        public HelloDarkness() : base()
        {
        }

        protected override void ApplyEffect()
        {
            currentRoom = Managers.GameManager.Instance.LevelManager.CurrentRoom;
            currentRoom.LeaveRoom += OnLeavingRoom;
            fogger = GameObject.Instantiate(fog, new Vector3(currentRoom.transform.position.x, 6.5f ,currentRoom.transform.position.z), Quaternion.identity);
            Debug.Log("Activated " + GetType().Name);
        }

        private void OnLeavingRoom(Room leaving, Room toEnter)
        {
            currentRoom.LeaveRoom -= OnLeavingRoom;
            Object.Destroy(fogger);
        }
    }
}
