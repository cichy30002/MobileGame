using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartSpawner : MonoBehaviour
{
    public GameObject rocket;
    public List<RandomSpawn> spawns;
    public Vector2 playerZoneLowerLeft;
    public Vector2 playerZoneUpperRight;
    
    private float _playerZoneWidth;
    private float _playerZoneHeight;
    private const float PlayerZoneThreshold = 1.2f;
    private const float PlayerThreshold = 3f;

    private void Start()
    {
        CalculatePlayerZone();
        //FillPlayerZone();
        InvokeRepeating(nameof(CalculatePlayerZone),0.2f,0.2f);
    }
    

    private void CalculatePlayerZone()
    {
        Camera main = Camera.main;
        float yHalfSize = main.orthographicSize * PlayerZoneThreshold;
        float xHalfSize = (float)main.pixelWidth / main.pixelHeight * main.orthographicSize * PlayerZoneThreshold;
        _playerZoneHeight = yHalfSize * 2f;
        _playerZoneWidth = xHalfSize * 2f;

        Vector3 rocketPosition = rocket.transform.position;
        playerZoneLowerLeft = new Vector2(rocketPosition.x - xHalfSize, rocketPosition.y - yHalfSize);
        playerZoneUpperRight = new Vector2(rocketPosition.x + xHalfSize, rocketPosition.y + yHalfSize);
    }

    private void FillPlayerZone()
    {
        foreach (RandomSpawn spawn in spawns)
        {
            for (int i = 0; i < spawn.amount; i++)
            {
                Vector2 randomPosition = new Vector2();
                do
                {
                    randomPosition.x = Random.Range(playerZoneLowerLeft.x, playerZoneUpperRight.x);
                    randomPosition.y = Random.Range(playerZoneLowerLeft.y, playerZoneUpperRight.y);
                } while (Vector2.Distance(randomPosition,rocket.transform.position) <= PlayerThreshold);

                Instantiate(spawn.prefab, randomPosition, quaternion.identity);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawWireCube(rocket.transform.position,new Vector3(_playerZoneWidth,_playerZoneHeight));
    }
    
    [Serializable]public struct RandomSpawn
    {
        public GameObject prefab;
        public int amount;
    }
}
