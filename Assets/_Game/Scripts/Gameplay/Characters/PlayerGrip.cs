using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Characters
{
    public class PlayerGrip : MonoBehaviour
    {
        private bool _isClose;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private List<Transform> _pastTransforms = new();
        private List<Rigidbody> _pastTigidbodies = new();
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                // _pastTransforms.Add(_transform);
                _isClose = true;
                // _transform = other.transform;
                _pastTransforms.Add(other.transform);
                _pastTigidbodies.Add(other.GetComponent<Rigidbody>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _isClose = false;
            _pastTransforms.Clear();
            _pastTigidbodies.Clear();
        }

        public void OnGripping()
        {
            if (_isClose)
            {
                foreach (var pastTransform in _pastTransforms)
                {
                    pastTransform.parent = this._transform;
                }
                foreach (var pastTigidbody in _pastTigidbodies)
                {
                    pastTigidbody.isKinematic = true;
                }
            }
            else
            {
                OnGrippingRelease();
            }
        }

        public void OnGrippingRelease()
        {
            foreach (var pastTransform in _pastTransforms)
            {
                pastTransform.parent = null;
            }
            _pastTransforms.Clear();
            
            foreach (var pastTigidbody in _pastTigidbodies)
            {
                pastTigidbody.isKinematic = false;
                pastTigidbody.WakeUp();
            }
            _pastTigidbodies.Clear();
        }
    }
}