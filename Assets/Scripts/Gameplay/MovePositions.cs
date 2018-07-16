using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositions : MonoBehaviour
{
    [SerializeField] private Vector3[] _positions;
    private int _desiredIndex;
    private int _currentIndex;

    [SerializeField] private float _timeToChangePositions = 1f;
    private bool _isMoving = false;

    [SerializeField] private AnimationCurve _moveAnimationCurve;

    private Animator _anim;

    void Awake()
    {
        if (_positions.Length <= 0)
            _positions = new Vector3[] { new Vector3(-1, 0, 0), new Vector3 (0, 0, 0), new Vector3(1, 0, 0) };

        _anim = GetComponent<Animator>();
        if (!_anim)
            Debug.LogError("Could not resolve Animator reference.");

        _anim.SetFloat("MoveSpeed", 1f / _timeToChangePositions);

        SetInitialPosition(Mathf.FloorToInt(_positions.Length / 2));
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
        if (_isMoving)
            return;

        if (i < 0 || i >= _positions.Length)
            return;

        if (_currentIndex > i)
            _anim.SetTrigger("MoveLeft");
        else if (_currentIndex < i)
            _anim.SetTrigger("MoveRight");

        _currentIndex = i;
        StartCoroutine(MoveToPosition(_currentIndex));
    }

    public void IncrementIndex()
    {
        _desiredIndex++;
    }

    public void DecrementIndex()
    {
        _desiredIndex--;
    }

    private IEnumerator MoveToPosition(int i)
    {
        _isMoving = true;

        Vector3 startPosition = transform.position;
        float startTime = Time.time - Time.deltaTime;
        float interpolationValue = 0f;
        while (interpolationValue <= 1f)
        {
            interpolationValue = (Time.time - startTime) / _timeToChangePositions;
            transform.position = Vector3.Lerp(startPosition, _positions[i], _moveAnimationCurve.Evaluate(interpolationValue));
            yield return null;
        }
        
        _isMoving = false;
    }
}
