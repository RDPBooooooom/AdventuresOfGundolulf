using LivingEntities;
using System.Collections;
using System.Collections.Generic;
using Managers;
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
        Cooldown = 2f;
        _targetEntity = targetEntity;
    }

    public override void Use()
    {
        if (IsReady)
        {
            DoTeleport();
        }
    }

    public void DoTeleport()
    {
        _owner.Animator.SetTrigger(AnimatorStrings.MagicString);

        TrimTargetPos();
        if(GameManager.Instance?.LevelManager?.CurrentRoom)
            TargetPos = GameManager.Instance.LevelManager.CurrentRoom.GetClosestPositionOnGround(TargetPos);
        
        Vector3 startPosition = _targetEntity.transform.position;

        OnTeleportStart?.Invoke(startPosition, TargetPos);
        _targetEntity.transform.position = TargetPos;
        OnTeleportEnd?.Invoke(startPosition, _targetEntity.transform.position);

        StartCooldown();
    }

    private void TrimTargetPos()
    {
        float teleportDistance = Vector3.Distance(_targetEntity.transform.position, TargetPos);
        
        if (teleportDistance < TeleportRange) return;

        Vector3 direction = (TargetPos - _targetEntity.transform.position).normalized;

        TargetPos -= (direction * (teleportDistance - TeleportRange));
    }

    #endregion
}
