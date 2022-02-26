using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Ability 
{
    #region Fields

    private LivingEntity _targetEntity;

    #endregion

    #region Properties

    public Vector3 TargetPos { get; set; }
    public int TeleportRange { get; set; } = 25;

    #endregion

    #region Delegates

    public delegate void TeleportHandler(Vector3 startPos, Vector3 endPos);

    #endregion

    #region Events

    public event TeleportHandler OnTeleportStart;
    public event TeleportHandler OnTeleportEnd;

    #endregion

    #region Methods

    public Teleport(LivingEntity owner, LivingEntity targetEntity) : base(owner)
    {
        cooldown = 2f;
        isReady = true;
        _targetEntity = targetEntity;
    }

    public override void Use()
    {
        if (isReady)
        {
            DoTeleport();
        }
    }

    public void DoTeleport()
    {
        TrimTargetPos();
        
        // TODO Implement cooldown
        Vector3 startPosition = _targetEntity.transform.position;

        OnTeleportStart?.Invoke(startPosition, TargetPos);
        //TODO Play Teleport animation

        // TODO Figure out a way to not teleport in to walls etc.
        _targetEntity.transform.position = TargetPos;

        //TODO Play landing animation

        OnTeleportEnd?.Invoke(startPosition, _targetEntity.transform.position);

        StartCooldown();
    }

    private void TrimTargetPos()
    {
        float teleportDistance = Vector3.Distance(_targetEntity.transform.position, TargetPos);
        
        if (teleportDistance < TeleportRange) return;
        
        Vector3 direction = TargetPos.normalized - _targetEntity.transform.position.normalized;


        TargetPos -= (direction * (teleportDistance - TeleportRange));
    }

    private bool IsValidTargetPosition()
    {
        int layerMask = LayerMask.GetMask("Floor");

        Ray ray = Camera.main.ScreenPointToRay(TargetPos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 500f, layerMask))
        {
            return true;
        }
        return false;
    }

    #endregion
}
