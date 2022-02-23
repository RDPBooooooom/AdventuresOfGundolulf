using System;
using LivingEntities;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Player : LivingEntity
    {
        #region Fields

        private PlayerInput _input;
        private Animator _animator;
        private Camera _camera;

        //TODO Introduce const for speed Multiplier
        [SerializeField] private float _speedMutliplier;

        #endregion

        #region Delegates

        public delegate void TeleportHandler(Vector3 startPos, Vector3 endPos);

        #endregion

        #region Events

        public event TeleportHandler OnTeleportStart;
        public event TeleportHandler OnTeleportEnd;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _input = new PlayerInput();
            SubscribeToEvents();
        }

        protected void Start()
        {
            _camera = Camera.main;
            _animator = GetComponent<Animator>();

            _input.Ingame.Enable();
        }

        protected void Update()
        {
            Movement();
            LookDirection();
        }

        #endregion

        #region Character Actions

        public void Teleport()
        {
            Teleport(GetCurrentMousePosInWorld());
        }

        public void Teleport(Vector3 targetPos)
        {
            // TODO Implement cooldown
            Vector3 startPosition = transform.position;
            
            OnTeleportStart?.Invoke(startPosition, targetPos);
            //TODO Play Teleport animation

            // TODO Figure out a way to not teleport in to walls etc.
            transform.position = targetPos;

            //TODO Play landing animation
            
            OnTeleportEnd?.Invoke(startPosition, transform.position);
        }

        public void Melee()
        {
        }

        public void CastSpell()
        {
        }

        #endregion

        #region Helper Methods

        private Vector3 GetCurrentMousePosInWorld()
        {
            Vector2 mousePos = _input.Ingame.MousePosition.ReadValue<Vector2>();
            if (Physics.Raycast(_camera.ScreenPointToRay(mousePos), out RaycastHit hitInfo))
            {
                return hitInfo.point;
            }
            //TODO Throw exception when failing to retrieve valid position
            return Vector3.zero;
        }

        #endregion

        #region Input

        private void Movement()
        {
            Vector2 inputVector = _input.Ingame.Movement.ReadValue<Vector2>();

            _animator.SetFloat(Animator.StringToHash("MoveX"), inputVector.x, 0.1f, Time.deltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), inputVector.y, 0.1f, Time.deltaTime);
            
            Vector3 direction = new Vector3(inputVector.x * Speed, 0, inputVector.y * Speed);

            transform.Translate(direction * Time.deltaTime * _speedMutliplier, Space.World);
        }

        private void LookDirection()
        {
            Vector3 worldPoint = GetCurrentMousePosInWorld();

            Debug.DrawLine(transform.position, worldPoint, Color.red);

            // ToDo: If looking at a wall, the player can look upwards which looks weird
            transform.LookAt(worldPoint);
        }


        private void PerformMelee(InputAction.CallbackContext context)
        {
            Debug.Log("Melee Input performed");
        }

        private void PerformSpellCast(InputAction.CallbackContext context)
        {
            Debug.Log("Spell Cast Input performed");
        }

        private void PerformTeleport(InputAction.CallbackContext context)
        {
            Debug.Log("Teleport Input performed");
            Teleport();
        }

        private void PerformInteract(InputAction.CallbackContext context)
        {
            Debug.Log("Interact Input performed");
        }

        private void PerformActiveItem(InputAction.CallbackContext context)
        {
            Debug.Log("Active Item Input performed");
        }

        #region Events

        private void SubscribeToEvents()
        {
            _input.Ingame.Melee.performed += PerformMelee;
            _input.Ingame.CastSpell.performed += PerformSpellCast;
            _input.Ingame.Teleport.performed += PerformTeleport;
            _input.Ingame.Interact.performed += PerformInteract;
            _input.Ingame.ActiveItem.performed += PerformActiveItem;
        }

        #endregion

        #endregion
    }
}