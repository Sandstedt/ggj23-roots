using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Effects
{
    public class TimedLifeParticle : MonoBehaviour
    {
        [SerializeField] float timedLife;

        void Awake()
        {
            //Start the coroutine we define below named ExampleCoroutine.
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