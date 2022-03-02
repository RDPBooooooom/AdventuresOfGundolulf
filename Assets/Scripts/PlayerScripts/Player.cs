using Assets.Scripts;
using Assets.Scripts.Interfaces;
using LivingEntities;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace PlayerScripts
{
    public class Player : LivingEntity
    {
        #region Fields

        private PlayerInput _input;
        private Animator _animator;
        private Camera _camera;
        private Rigidbody _rigidbody;
        
        private Teleport _teleport;
        private Melee _melee;
        private SpellCast _spellCast;
        private ActiveItem _activeItem;

        private int _groundLayer;
        [SerializeField] private float _interactRange;
        
        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _input = new PlayerInput();
            _teleport = new Teleport(this, this);
            _melee = new Melee(this);
            _spellCast = new SpellCast(this);
            _activeItem = null;

            _groundLayer = LayerMask.GetMask("Floor");
            
            SubscribeToEvents();

        }

        protected void Start()
        {
            _camera = Camera.main;
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

            _input.Ingame.Enable();
            UserInterface.InGameUI.Instance.HealthDisplayBar.fillAmount = Health / 100;
        }

        protected void FixedUpdate()
        {
            Movement();
            LookDirection();
            GetInteractableObject();
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

        private Vector3 GetCurrentMousePosInWorldOnGround()
        {
            Vector2 mousePos = _input.Ingame.MousePosition.ReadValue<Vector2>();
            if (Physics.Raycast(_camera.ScreenPointToRay(mousePos), out RaycastHit hitInfo, 1000, _groundLayer))
            {
                return hitInfo.point;
            }
            //TODO Find clostest point to ground
            return Vector3.zero;
        }

        private IInteractable GetInteractableObject()
        {
            Collider[] closeObjects = Physics.OverlapSphere(transform.position, _interactRange);

            IInteractable closestInteractable = null;
            float closestDistance = float.MaxValue;

            foreach (Collider closeObject in closeObjects)
            {
                IInteractable currentObject = closeObject.GetComponent<IInteractable>();

                if (currentObject != null)
                {
                    float distance = Vector3.Distance(transform.position, closeObject.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = currentObject;
                    }
                }
            }

            return closestInteractable;
        }

        #endregion

        #region Character actions

        private void InteractWithObject()
        {
            // ToDo: Display Text if you can interact with something

            IInteractable interactableObject = GetInteractableObject();

            if (interactableObject == null)
            {
                return;
            }

            interactableObject.Interact();
        }

        private void UseItem()
        {
            IUsable usableObject = (IUsable)_activeItem;

            usableObject.Use();
        }

        #endregion

        #region Input

        private void Movement()
        {
            Vector2 inputVector = _input.Ingame.Movement.ReadValue<Vector2>();

            _animator.SetFloat(Animator.StringToHash("MoveX"), inputVector.x, 0.1f, Time.deltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), inputVector.y, 0.1f, Time.deltaTime);
            
            Vector3 direction = new Vector3(inputVector.x * Speed, 0, inputVector.y * Speed);
            
            _rigidbody.AddForce(direction * (Time.fixedDeltaTime * GameConstants.SpeedMultiplier), ForceMode.VelocityChange);
        }

        private void LookDirection()
        {
            Vector3 worldPoint = GetCurrentMousePosInWorldOnGround();

            Debug.DrawLine(transform.position, new Vector3(worldPoint.x, 0, worldPoint.z), Color.red);
            
            transform.LookAt(worldPoint);
        }


        private void PerformMelee(InputAction.CallbackContext context)
        {
            Debug.Log("Melee Input performed");
            _melee.Use();
        }

        private void PerformSpellCast(InputAction.CallbackContext context)
        {
            Debug.Log("Spell Cast Input performed");
            _spellCast.Use();
        }

        private void PerformTeleport(InputAction.CallbackContext context)
        {
            Debug.Log("Teleport Input performed");
            _teleport.TargetPos = GetCurrentMousePosInWorldOnGround();
            _teleport.Use();
        }

        private void PerformInteract(InputAction.CallbackContext context)
        {
            Debug.Log("Interact Input performed");
            InteractWithObject();
        }

        private void PerformActiveItem(InputAction.CallbackContext context)
        {
            Debug.Log("Active Item Input performed");
            UseItem();
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

        #region Override Methods

        public override void HealEntity(float amount)
        {
            base.HealEntity(amount);

            UserInterface.InGameUI.Instance.HealthDisplayBar.fillAmount = Health / 100;
        }
        public override void DamageEntity(float amount)
        {
            base.DamageEntity(amount);
            UserInterface.InGameUI.Instance.HealthDisplayBar.fillAmount = Health / 100;
        }
        #endregion
    }
}