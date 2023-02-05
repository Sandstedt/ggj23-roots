using Assets._Game.Scripts.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class TimedLifeParticle : MonoBehaviour
    {
        [SerializeField] float timedLife;
        [SerializeField] RandomSoundPlay randomSoundPlay;

        void Awake()
        {
            //Start the coroutine we define below named ExampleCoroutine.
            if (randomSoundPlay != null)
            {
                randomSoundPlay.PlayRandomSound();
            }
            StartCoroutine(DieAfterTime());
        }

        IEnumerator DieAfterTime()
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(timedLife);

            Destroy(gameObject);

        }
    }
}