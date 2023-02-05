using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Characters
{
    public class PlayerGrip : MonoBehaviour
    {
        private bool _isClose;
        private Transform _transform;
        private Rigidbody _rigidbody;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                _isClose = true;
                _transform = other.transform;
                _rigidbody = other.GetComponent<Rigidbody>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isClose = false;
        }

        public void OnGripping()
        {
            if (_isClose)
            {
                _transform.parent = this.transform;
                _rigidbody.isKinematic = true;
            }
            else
            {
                _transform.parent = null;
                _rigidbody.isKinematic = false;
                _rigidbody.WakeUp();
            }
        }

        public void OnGrippingRelease()
        {
            if (_rigidbody)
            {
                _transform.parent = null;
                _rigidbody.isKinematic = false;
                _rigidbody.WakeUp();
            }
        }
    }
}