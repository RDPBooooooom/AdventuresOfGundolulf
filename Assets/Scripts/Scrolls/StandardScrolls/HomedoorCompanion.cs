using UnityEngine;

namespace Scrolls.StandardScrolls
{
    public class HomedoorCompanion : StandardScroll
    {
        GameObject Door = Resources.Load<GameObject>("Prefabs/Door");
        public HomedoorCompanion() : base()
        {

        }

        protected override void ApplyEffect()
        {
            SpawnDoor();
            Debug.Log("Activated " + GetType().Name);
        }

        void SpawnDoor()
        {
            GameObject.Instantiate(Door, Managers.GameManager.Instance.LevelManager.CurrentRoom.transform.position, Quaternion.identity);
        }
    }
}
