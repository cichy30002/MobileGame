using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform rocket;
    [SerializeField] private float smoothTime = 0.45f;
    private Vector3 _velocity = Vector3.zero;
    private readonly Vector3 _offset = new Vector3(0f, 0f, -10f);

    private void Update()
    {
        Vector3 targetPos = rocket.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }
}
