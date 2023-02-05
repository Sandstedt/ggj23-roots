using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Debugging
{
    public class NonHeadsetGameplay : MonoBehaviour
    {
        [SerializeField] List<GameObject> listEnable;
        [SerializeField] List<GameObject> listDisable;

        void Start()
        {
            foreach (var item in listEnable)
            {
                item.SetActive(true);
            }

            foreach (var item in listDisable)
            {
                item.SetActive(false);
            }
        }
    }
}