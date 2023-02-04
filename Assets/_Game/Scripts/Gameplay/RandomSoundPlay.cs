using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay
{
    public class RandomSoundPlay : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> listSounds;

        public void PlayRandomSound()
        {
            if (listSounds.Count > 0)
            {
                audioSource.clip = listSounds[Random.Range(0, listSounds.Count)];
                audioSource.Play();
            }
        }
    }
}