using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _toFollow;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - _toFollow.position;
    }

    void Update()
    {
        transform.position = _toFollow.position + _offset;
    }
}
