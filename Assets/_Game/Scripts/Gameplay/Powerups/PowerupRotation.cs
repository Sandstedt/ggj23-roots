using System.Collections;
using UnityEngine;

namespace Assets._Game.Scripts.Gameplay.Powerups
{
    public class PowerupRotation : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 20f;
        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}