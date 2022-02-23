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
        this._targetEntity = targetEntity;
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

    #endregion
}
