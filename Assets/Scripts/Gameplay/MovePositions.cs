﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;

namespace AsteroidRage.Game
{
    public class MovePositions : MonoBehaviour
    {
        [SerializeField] private Vector3[] _positions;
        private int _desiredIndex;
        private int _currentIndex;

        [SerializeField] private float _moveSpeed = 5f;

        private enum MoveState { None, Left, Right };
        private MoveState _moveState = MoveState.None;

        [SerializeField] private AnimationCurve _moveAnimationCurve;

        private Animator _anim;

        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt CountChanged;
        }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent DodgedAsteroid;
            public GameEventInt AddToScore;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] InvokeEvents _invokeEvents;

        [SerializeField] DifficultyConfig _diffConfig;
        private float _moveSpeedScale = 1.0f;

        void Awake()
        {
            if (_positions.Length <= 0)
                _positions = new Vector3[] { new Vector3(-1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0) };

            _anim = GetComponent<Animator>();
            if (!_anim)
                Debug.LogError("Could not resolve Animator reference.");

            _anim.SetFloat("MoveSpeed", _moveSpeed * _moveSpeedScale);

            SetInitialPosition(Mathf.FloorToInt(_positions.Length / 2));

            _responseEvents.CountChanged.AddListener(ScaleUp);
        }

        void LateUpdate()
        {
            if (_desiredIndex != _currentIndex)
            {
                SwitchPosition(_desiredIndex);
                _desiredIndex = _currentIndex;
            }
        }

        private void SetInitialPosition(int i)
        {
            if (i < 0 || i >= _positions.Length)
                return;

            _desiredIndex = i;
            _currentIndex = i;
            transform.position = _positions[i];
        }

        public void SwitchPosition(int i)
        {
            if (i < 0 || i >= _positions.Length)
                return;

            if (i == _currentIndex || (i < _currentIndex && _moveState == MoveState.Left) || (i > _currentIndex && _moveState == MoveState.Right))
                return;

            int oldIndex = _currentIndex;
            _currentIndex = i;

            if (_currentIndex < oldIndex)
                StartCoroutine(MoveToPosition(_currentIndex, MoveState.Left));
            else
                StartCoroutine(MoveToPosition(_currentIndex, MoveState.Right));
        }

        public void IncrementIndex()
        {
            _desiredIndex++;
        }

        public void DecrementIndex()
        {
            _desiredIndex--;
        }

        private IEnumerator MoveToPosition(int i, MoveState moveState)
        {
            _moveState = moveState;

            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, _diffConfig.DodgeDistance))
            {
                if (hit.collider.CompareTag("Asteroid"))
                {
                    _invokeEvents.DodgedAsteroid.Invoke();
                    _invokeEvents.AddToScore.Invoke(_diffConfig.DodgeScore);
                }
            }

            switch (moveState)
            {
                case MoveState.None:
                    break;
                case MoveState.Left:
                    _anim.SetTrigger("MoveLeft");
                    break;
                case MoveState.Right:
                    _anim.SetTrigger("MoveRight");
                    break;
                default:
                    break;
            }

            Vector3 startPosition = transform.position;
            float startTime = Time.time - Time.deltaTime;
            float interpolationValue = 0f;
            while (interpolationValue <= 1f)
            {
                interpolationValue = (Time.time - startTime) * _moveSpeed * _moveSpeedScale;
                transform.position = Vector3.Lerp(startPosition, _positions[i], _moveAnimationCurve.Evaluate(interpolationValue));
                yield return null;
            }

            _moveState = MoveState.None;
        }

        public void ScaleUp(int count)
        {
            _moveSpeedScale = 1f + _diffConfig.MoveSpeedScaleStep * Mathf.Round(count / _diffConfig.MoveSpeedScaleInterval);
        }
    }
}
