using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAudio : MonoBehaviour
{ 
    [SerializeField] private AudioSource source;
    [SerializeField] private float minDist = 1f;
    [SerializeField] private float maxDist = 20f;
    private Vector3 _cameraPosition;
    private void Start()
    {
        _cameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        source.volume = CalculateVolume();
    }

    private float CalculateVolume()
    {
        float dist = Vector2.Distance(new Vector2(_cameraPosition.x, _cameraPosition.y),
            transform.position);
        return (maxDist-dist) / (maxDist-minDist);
    }
}
