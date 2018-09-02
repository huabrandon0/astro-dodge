using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AsteroidRage.Game
{
    public class StandingTrail : MonoBehaviour
    {
        LineRenderer _lineRenderer;
        LinkedList<Vector3> _positions;

        public Vector3 _direction;
        public float _speed;
        public float _timeToLive;
        //public float _timeBetweenPositions;

        bool _trailOn = true;

        void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _positions = new LinkedList<Vector3>();
        }

        void OnDisable()
        {
            Reset();
        }

        void OnEnable()
        {
            Reset();
        }

        public void Reset()
        {
            _positions.Clear();
            _lineRenderer.positionCount = 0;
        }

        public void Off()
        {
            Reset();
            _trailOn = false;
        }

        public void On()
        {
            Reset();
            _trailOn = true;
        }

        void LateUpdate()
        {
            if (_trailOn)
            {
                for (LinkedListNode<Vector3> node = _positions.First; node != null;)
                {
                    node.Value += _direction.normalized * _speed * Time.deltaTime;
                    node = node.Next;
                }

                _positions.AddFirst(transform.position);
                StartCoroutine(RemoveLastPosition(_timeToLive));

                _lineRenderer.positionCount = _positions.Count;
                _lineRenderer.SetPositions(_positions.ToArray());
            }
        }

        IEnumerator RemoveLastPosition(float time)
        {
            yield return new WaitForSeconds(time);
            _positions.RemoveLast();
        }
    }
}
