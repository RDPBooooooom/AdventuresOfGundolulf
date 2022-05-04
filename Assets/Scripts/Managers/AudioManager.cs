using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        #region Declaring Variables
        AudioSource attackSword;
        AudioSource attackMagic;
        AudioSource teleport;
        AudioSource door;
        AudioSource ouch;
        AudioSource ouchOrc;
        List<AudioSource> audioSources;
        #endregion

        private void Start()
        {
            GetAudioClips();
            audioSources = new List<AudioSource> { attackSword, attackMagic, teleport, door, ouch, ouchOrc };
            SetSoundsValue(Utils.SharedValues.Volume);
            GameManager.Instance.UIManager.MainCanvas.GetComponent<UI.InGameUI>().audioSlider.value = Utils.SharedValues.Volume;
        }

        void GetAudioClips()
        {
            attackSword = Instantiate(Resources.Load<AudioSource>("Prefabs/AudioSources/SwordSlash"), transform);
            attackMagic = Instantiate(Resources.Load<AudioSource>("Prefabs/AudioSources/SpellCast"), transform);
            teleport = Instantiate(Resources.Load<AudioSource>("Prefabs/AudioSources/Teleport"), transform);
            door = Instantiate(Resources.Load<AudioSource>("Prefabs/AudioSources/Door"), transform);
            ouch = Instantiate(Resources.Load<AudioSource>("Prefabs/AudioSources/Ouch"), transform);
            ouchOrc = Instantiate(Resources.Load<AudioSource>("Prefabs/AudioSources/OuchOrc"), transform);
        }

        public void SetSoundsValue(float volume)
        {
            foreach (AudioSource audio in audioSources)
                audio.volume = volume;
            Utils.SharedValues.SetVolume(volume);
        }

        #region Playing clips
        public void PlayAttackSound()
        {
            attackSword.Play();
        }
        public void PlayMagicSound()
        {
            attackMagic.Play();
        }
        public void PlayteleportSound()
        {
            teleport.Play();
        }
        public void PlayDoorSound()
        {
            door.Play();
        }
        public void PlayOuchSound()
        {
            ouch.Play();
        }
        public void PlayOuchOrcSound()
        {
            ouchOrc.Play();
        }
    }
    #endregion
}
