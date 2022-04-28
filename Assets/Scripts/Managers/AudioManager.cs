using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        #endregion

        private void Start()
        {
            GetAudioClips();
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
}
