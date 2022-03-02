using UnityEditor;
using UnityEngine;

namespace Levels.Rooms
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RoomData", order = 1)]
    public class RoomData : ScriptableObject
    {
        [field: SerializeField] public DoorDirections Directions { get; set; }
    }
}
