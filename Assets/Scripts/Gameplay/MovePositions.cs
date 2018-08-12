using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidRage.Events;
using AsteroidRage.Extensions;

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

        private Coroutine _moveCoroutine;

        [SerializeField] private AnimationCurve _moveAnimationCurve;

        //private Animator _anim;

        private bool _canMove = false;

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
            public GameEvent PlayerMoved;
            public GameEvent PlayerMovedLeft;
            public GameEvent PlayerMovedRight;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] InvokeEvents _invokeEvents;

        [SerializeField] DifficultyConfig _diffConfig;
        private float _moveSpeedScale = 1.0f;

        void Awake()
        {
            if (_positions.Length <= 0)
                _positions = new Vector3[] { new Vector3(-1, 0, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0) };

            //_anim = GetComponent<Animator>();
            //if (!_anim)
            //    Debug.LogError("Could not resolve Animator reference.");

            //_anim.SetFloat("MoveSpeed", _moveSpeed * _moveSpeedScale);

            _responseEvents.CountChanged.AddListener(ScaleUp);
        }

        void Start()
        {
            SetRandomPosition();
        }

        void LateUpdate()
        {
            if (_desiredIndex != _currentIndex)
            {
                SwitchPosition(_desiredIndex);
                _desiredIndex = _currentIndex;
            }
        }

        void SetPosition(int i)
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

            //if (i == _currentIndex || (i < _currentIndex && _moveState == MoveState.Left) || (i > _currentIndex && _moveState == MoveState.Right))
            //    return;
            if (i == _currentIndex || _moveState != MoveState.None)
                return;

            int oldIndex = _currentIndex;
            _currentIndex = i;

            if (_moveCoroutine != null)
                StopCoroutine(_moveCoroutine);

            if (_currentIndex < oldIndex)
                _moveCoroutine = StartCoroutine(MoveToPosition(_currentIndex, MoveState.Left));
            else
                _moveCoroutine = StartCoroutine(MoveToPosition(_currentIndex, MoveState.Right));

            _invokeEvents.PlayerMoved.Invoke();
        }

        public void IncrementIndex()
        {
            if (_canMove)
            {
                _desiredIndex++;
            }
        }

        public void DecrementIndex()
        {
            if (_canMove)
                _desiredIndex--;
        }

        public void Disable()
        {
            _canMove = false;
        }

        public void Enable()
        {
            _canMove = true;
        }

        public void Enable(float time)
        {
            StartCoroutine(EnableAfterSeconds(time));
        }

        IEnumerator EnableAfterSeconds(float time)
        {
            yield return new WaitForSeconds(time);
            Enable();
        }

        IEnumerator MoveToPosition(int i, MoveState moveState)
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
                    _invokeEvents.PlayerMovedLeft.Invoke();
                    //_anim.SetTrigger("MoveLeft");
                    break;
                case MoveState.Right:
                    _invokeEvents.PlayerMovedRight.Invoke();
                    //_anim.SetTrigger("MoveRight");
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
            float oldMoveSpeedScale = _moveSpeedScale;
            float desiredMoveSpeedScale = 1f + _diffConfig.MoveSpeedScaleStep * Mathf.Round(count / _diffConfig.MoveSpeedScaleInterval);
            _moveSpeedScale = Mathf.Min(desiredMoveSpeedScale, _diffConfig.MoveSpeedScaleMax);

            //if (_moveSpeedScale != oldMoveSpeedScale)
            //    _anim.SetFloat("MoveSpeed", _moveSpeed * _moveSpeedScale);
        }

        public void ResetPosition()
        {
            SetPosition(Mathf.FloorToInt(_positions.Length / 2));
        }

        public void SetRandomPosition()
        {
            SetPosition(Random.Range(0, _positions.Length));
        }
    }
}
