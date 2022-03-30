﻿using System;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Items.Active;
using LivingEntities;
using Managers;
using UI;
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

        private Teleport _teleport;
        private Melee _melee;
        private SpellCast _spellCast;
        private ActiveItem _activeItem;

        private int _groundLayer;
        
        [Header("Player")]
        [SerializeField] private float _interactRange;
        
        private int _gold;

        #endregion

        #region Properties

        public int Gold
        {
            get => _gold;
            set
            {
                _gold = value;
                UpdateGoldEvent?.Invoke();
            }
        }

        #endregion

        #region Delegates

        public delegate void UpdateHealthHandler();

        public delegate void UpdateGoldHandler(); 

        #endregion

        #region Events

        public event UpdateHealthHandler UpdateHealthEvent;
        public event UpdateGoldHandler UpdateGoldEvent;

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

        protected override void Start()
        {
            base.Start();
            _steeringBehaviour.SeekOn();
            _camera = Camera.main;
            _animator = GetComponent<Animator>();

            _input.Ingame.Enable();
            UpdateHealthEvent?.Invoke();
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
            if (_activeItem != null)
            {
                IUsable usableObject = (IUsable)_activeItem;

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

            if (GameManager.Instance?.LevelManager?.CurrentRoom &&
                GameManager.Instance.LevelManager.CurrentRoom.IsPositionInRoom(worldPoint))
            {
                Debug.DrawLine(transform.position, new Vector3(worldPoint.x, 0, worldPoint.z), Color.green);
            }
            else
            {
                Debug.DrawLine(transform.position, new Vector3(worldPoint.x, 0, worldPoint.z), Color.red);
            }

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

            UpdateHealthEvent?.Invoke();
        }

        public override void DamageEntity(float amount)
        {
            base.DamageEntity(amount);

            UpdateHealthEvent?.Invoke();
        }

        #endregion
    }
}