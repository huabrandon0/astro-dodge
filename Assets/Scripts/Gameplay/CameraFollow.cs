using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _toFollow;
    private Vector3 _offset;
    public float _smoothSpeed = 0.5f;

    void Start()
    {
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
