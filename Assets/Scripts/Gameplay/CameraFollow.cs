using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidRage.Game
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform _toFollow;
        private Vector3 _offset;
        public float _smoothSpeed = 0.5f;

        void Start()
        {
            if (!_toFollow)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (!player)
                {
                    gameObject.SetActive(false);
                    return;
                }
                else
                    _toFollow = player.transform;
            }

            _offset = transform.position - _toFollow.position;
        }

        void FixedUpdate()
        {
            Vector3 desiredPosition = _toFollow.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }

        public void SetExact()
        {
            transform.position = _toFollow.position + _offset;
        }
    }
}