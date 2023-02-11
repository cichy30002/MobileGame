using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform rocket;
    [SerializeField] private float smoothTime = 0.45f;
    [SerializeField] private float shakeDecay = 0.03f;
    
    private Vector3 _velocity = Vector3.zero;
    private float _shakePower = 0f;
    private readonly Vector3 _offset = new Vector3(0f, 0f, -10f);

    private void Update()
    {
        transform.position = CameraFollow() + CalculateShake();
        _shakePower = math.max(_shakePower - shakeDecay, 0f);
    }

    private Vector3 CameraFollow()
    {
        Vector3 targetPos = rocket.position + _offset;
        return Vector3.SmoothDamp(transform.position, targetPos, ref _velocity, smoothTime);
    }

    private Vector3 CalculateShake()
    {
        return new Vector3(Random.Range(-_shakePower, _shakePower), Random.Range(-_shakePower, _shakePower), 0f);
    }

    public void Shake(float power)
    {
        _shakePower += power;
    }
    
}
