using UnityEngine;
using Utils;
using PlayerScripts;
using Managers;
using System.Collections;
using Levels.Rooms;
using LivingEntities;
using Microsoft.Unity.VisualStudio.Editor;
using UI;
using UnityEngine.Assertions.Must;
using Image = UnityEngine.UI.Image;

namespace Scrolls.StandardScrolls
{
    public class PoisonGas : StandardScroll
    {
        #region Fields

        private Player _player;
        private CombatRoom _combatRoom;
        private InGameUI _inGameUI;
        private Image _image;

        private Coroutine _effect;
        
        private float _duration = 15;

        #endregion

        #region Constructor

        public PoisonGas() : base()
        {            
        }

        #endregion

        #region Effect

        protected override void ApplyEffect()
        {
            Debug.Log("Activated " + GetType().Name);
            _player = GameManager.Instance.Player;
            _combatRoom = (CombatRoom)GameManager.Instance.LevelManager.CurrentRoom;
            
            _inGameUI = GameManager.Instance.UIManager.MainCanvas.GetComponent<InGameUI>();
            _image = _inGameUI.IngamePanel.GetComponent<Image>();
            _image.enabled = true;
            
            _effect = MonoBehaviourDummy.Dummy.StartCoroutine(EffectTick());
        }

        protected IEnumerator EffectTick()
        {
            float startTime = Time.time;
            while (Time.time - startTime < _duration)
            {
                if (_combatRoom.Cleared)
                {
                    MonoBehaviourDummy.Dummy.StopCoroutine(_effect);
                    _image.enabled = false;
                    break;
                }

                yield return new WaitForSeconds(1);

                _player.DamageEntity(1);
                foreach (LivingEntity entity in _combatRoom.Enemies)
                {
                    entity.DamageEntity(1);
                }
            }
        }

        #endregion
    }
}
