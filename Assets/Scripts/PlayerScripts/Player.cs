using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Items.Active;
using LivingEntities;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace PlayerScripts
{
    public class Player : LivingEntity
    {
        #region Fields
        
        [SerializeField] private int _gold;

        private PlayerInput _input;
        private Camera _camera;

        private Teleport _teleport;
        private Melee _melee;
        private SpellCast _spellCast;
        private ActiveItem _activeItem;

        private int _groundLayer;
        
        [Header("Player")]
        [SerializeField] private float _interactRange;

        private bool _stopMovement = false;
        private bool _invincible = false;
        private bool _pacifist = false;
        private bool _weeny = false;
        private bool _notWeeny = false;

        #endregion

        #region Properties

        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                OnUpdateGoldEvent?.Invoke();
            }
        }

        public bool StopMovement
        {
            get => _stopMovement;
            set => _stopMovement = value;
        }

        public SpellCast SpellCast
        {
            get => _spellCast;
            protected set => _spellCast = value;
        }

        public Teleport Teleport
        {
            get => _teleport;
            set => _teleport = value;
        }

        public PlayerInput Input
        {
            get => _input;
            set => _input = value;
        }

        public bool Invincible
        {
            set => _invincible = value;
        }

        public bool Pacifist
        {
            set => _pacifist = value;
        }

        public bool Weeny
        {
            set => _weeny = value;
        }

        public bool NotWeeny
        {
            set => _notWeeny = value;
        }


        public ActiveItem ActiveItem
        {
            get => _activeItem;
            private set
            {
                _activeItem = value;
                OnUpdateActiveItemEvent?.Invoke();
            }
        }

        #endregion

        #region Events

        public event StatUpdateHandler OnUpdateGoldEvent;
        public event StatUpdateHandler OnUpdateActiveItemEvent;

        #endregion
        

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            _input = new PlayerInput();
            _teleport = new Teleport(this, this);
            _melee = new Melee(this);
            _spellCast = new SpellCast(this);
            ActiveItem = null;

            _groundLayer = LayerMask.GetMask("Floor");

            SubscribeToEvents();
        }

        protected override void Start()
        {
            base.Start();
            _steeringBehaviour.SeekOn();
            _camera = Camera.main;

            _input.Ingame.Enable();
            UpdateStats();
        }

        protected void FixedUpdate()
        {
            UpdateVelocity();
            MoveEntity();
            LookDirection();
            GetInteractableObject();
        }

        private void OnDestroy()
        {
            _input.Ingame.Disable();
        }

        #endregion

        #region Helper Methods

        private Vector3 GetCurrentMousePosInWorld()
        {
            Vector2 mousePos = _input.Ingame.MousePosition.ReadValue<Vector2>();
            Ray ray = _camera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                return hitInfo.point;
            }

            return GetBackupPointInWorld(ray);
        }

        private Vector3 GetCurrentMousePosInWorldOnGround()
        {
            Vector2 mousePos = _input.Ingame.MousePosition.ReadValue<Vector2>();
            Ray ray = _camera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, _groundLayer))
            {
                return hitInfo.point;
            }

            return GetBackupPointInWorld(ray);
        }

        private Vector3 GetBackupPointInWorld(Ray ray)
        {
            //Setup Plane
            Plane plane = new Plane();
            plane.SetNormalAndPosition(Vector3.up, Vector3.zero);

            // Cast ray on plane
            if (plane.Raycast(ray, out float enter))
            {
                return ray.GetPoint(enter);
            }

            // No Point found
            Debug.LogError("No point was found!!!");
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

        /// <summary>
        /// Gets called if the animation for picking up an item is finished
        /// </summary>
        private void PickingUpFinished()
        {
            _stopMovement = false;
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
            if (ActiveItem != null)
            {
                IUsable usableObject = (IUsable)ActiveItem;

                usableObject.Use();
            }
        }

        #endregion

        #region Input

        public override void UpdateVelocity()
        {
            ResetVelocity();
            Vector2 inputVector = _input.Ingame.Movement.ReadValue<Vector2>();
            
            _animator.SetFloat(Animator.StringToHash("MoveX"), inputVector.x, 0.1f, Time.fixedDeltaTime);
            _animator.SetFloat(Animator.StringToHash("MoveZ"), inputVector.y, 0.1f, Time.fixedDeltaTime);

            inputVector.Normalize();
            Vector3 direction = new Vector3(inputVector.x * 1000, 0, inputVector.y * 1000);

            Vector3 position = transform.position;

            Vector3 steeringForce = _steeringBehaviour.Calculate(position + direction);

            Vector3 accel = steeringForce / Mass; // F = m * a => a = F / m. [a] = m/s^2

            Velocity += accel;

            Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);
        }

        public override void MoveEntity()
        {
            transform.Translate(Velocity * Time.deltaTime, Space.World);
        }

        private void LookDirection()
        {
            Vector3 worldPoint = GetCurrentMousePosInWorldOnGround();
            worldPoint = new Vector3(worldPoint.x, 0.5f, worldPoint.z);

            Vector3 pos = transform.position;

            Vector3 direction = worldPoint - new Vector3(pos.x, 0.5f, pos.z);

            if ((direction - Vector3.up).magnitude < 1.1f) return;
            if (direction.magnitude < 7.5f)
            {
                worldPoint += direction.normalized * 3;
            }

            if (GameManager.Instance?.LevelManager?.CurrentRoom &&
                GameManager.Instance.LevelManager.CurrentRoom.IsPositionInRoom(worldPoint))
            {
                Debug.DrawLine(transform.position, worldPoint, Color.cyan);
            }
            else
            {
                Debug.DrawLine(transform.position, worldPoint, Color.red);
            }

            transform.LookAt(worldPoint);
        }


        private void PerformMelee(InputAction.CallbackContext context)
        {
            if(!_pacifist && !_weeny)
            {
                Debug.Log("Melee Input performed");
                _melee.Use();
            }
        }

        private void PerformSpellCast(InputAction.CallbackContext context)
        {
            if(!_pacifist && !_notWeeny)
            {
                Debug.Log("Spell Cast Input performed");
                _spellCast.Use();
            }
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

        #endregion
        
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

        #region Override Methods

        public override void HealEntity(float amount)
        {
            base.HealEntity(amount);
        }

        public override void DamageEntity(float amount)
        {
            if(!_invincible)
            {
                base.DamageEntity(amount);
            }
        }

        #endregion
    }
}