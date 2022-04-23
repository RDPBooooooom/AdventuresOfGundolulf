using System;
using Assets.Scripts;
using Managers;
using PlayerScripts;
using UnityEngine;

namespace Items
{
    public class DroppedItem : MonoBehaviour, IInteractable
    {
        #region Fields

        [SerializeField] private float _upDownDifference = 0.5f;
        [SerializeField] private float _groundOffset = 0.5f;
        [SerializeField] private float _upDownSpeed = 0.25f;
        [SerializeField] private float _rotationSpeed = 30;

        private Player _player;
        private float _maxUpDownPos;
        private float _minUpDownPos;
        private bool _isUp = true;
        
        #endregion

        #region Properties

        public Item Item { get; set; }

        #endregion

        #region Interaction

        public void Interact()
        {
            _player.Equip(Item);
            _player.Animator.SetTrigger(AnimatorStrings.PickUpString);
            _player.StopMovement = true;

            Destroy(gameObject);
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _player = GameManager.Instance?.Player;

            if(Physics.Raycast(transform.position, Vector3.down,out RaycastHit hit, LayerMask.GetMask("Floor")))
            {
                _minUpDownPos = hit.point.y + _groundOffset;
                _maxUpDownPos = _minUpDownPos + _upDownDifference;
            }
        }

        private void Update()
        {
            UpDownAnimation();
            SpinningAnimation();
        }

        #endregion

        #region Animation

        private void UpDownAnimation()
        {
            if (_isUp)
            {
                if (transform.position.y > _maxUpDownPos) _isUp = false;
                transform.position += new Vector3(0, _upDownSpeed * Time.deltaTime, 0);
            }
            else
            {
                if (transform.position.y < _minUpDownPos) _isUp = true;
                transform.position -= new Vector3(0, _upDownSpeed * Time.deltaTime, 0);
            }

        }

        private void SpinningAnimation()
        {
            transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime, Space.World);
        }

        #endregion
    }
}