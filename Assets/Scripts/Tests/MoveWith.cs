using UnityEditorInternal;
using UnityEngine;

namespace Tests
{
    public class MoveWith : MonoBehaviour
    {

        [SerializeField] private Transform _moveWith;
        // Start is called before the first frame update
        void Start()
        {
            transform.position = GetPos();
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = GetPos();
        }

        private Vector3 GetPos()
        {
            Vector3 position = _moveWith.position;
            return new Vector3(position.x, transform.position.y, position.z);
        }
    }
}
