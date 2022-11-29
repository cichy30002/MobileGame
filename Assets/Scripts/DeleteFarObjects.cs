using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFarObjects : MonoBehaviour
{
    private Transform _rocket;
    private const float DistanceToDestroy = 25f;
    private void Start()
    {
        _rocket = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(CheckDistToPlayer),0.5f,1f);
    }

    private void CheckDistToPlayer()
    {
        if (Vector2.Distance(transform.position, _rocket.position) >= DistanceToDestroy)
        {
            Destroy(gameObject);
        }
    }
    
}
