using LivingEntities;
using UnityEngine;
using Utils;

namespace Abilities
{
    public class Block : Ability
    {
        private Timer _blockTime;

        public Block(LivingEntity owner, float blockTime) : base(owner)
        {
            Cooldown = 100 / owner.Haste;
            _blockTime = new Timer(owner, blockTime);
            _blockTime.OnTimerReady += UndoEffect;
        }

        public override void Use()
        {
            if (!IsReady) return;

            _owner.Invincible = true;
            _blockTime.Start();
            OnAbilityFinshed();
            Debug.Log("Blocking");
        }

        private void UndoEffect()
        {
            _owner.Invincible = false;
            Debug.Log("Finshed Blocking");
        }
    }
}