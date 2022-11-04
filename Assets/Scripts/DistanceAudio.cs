using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAudio : MonoBehaviour
{
    public AudioSource source;
    public float minDist = 1f;
    public float maxDist = 20f;
    private Transform _cameraTransform;
    private void Start()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        float dist = Vector2.Distance(new Vector2(_cameraTransform.position.x, _cameraTransform.position.y),
            transform.position);
        source.volume = (maxDist-dist) / (maxDist-minDist);
    }
}
