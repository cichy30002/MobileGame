using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private List<RandomSpawn> spawns;
    public Vector2 PlayerZoneLowerLeft{ get; private set; }
    public Vector2 PlayerZoneUpperRight{ get; private set; }
    
    private float _playerZoneWidth;
    private float _playerZoneHeight;
    private const float PlayerZoneThreshold = 1.2f;
    private const float PlayerThreshold = 3f;
    private void Start()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.startPlaying.AddListener(StartGame);
    }

    private void StartGame()
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
        PlayerZoneLowerLeft = new Vector2(rocketPosition.x - xHalfSize, rocketPosition.y - yHalfSize);
        PlayerZoneUpperRight = new Vector2(rocketPosition.x + xHalfSize, rocketPosition.y + yHalfSize);
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
                    randomPosition.x = Random.Range(PlayerZoneLowerLeft.x, PlayerZoneUpperRight.x);
                    randomPosition.y = Random.Range(PlayerZoneLowerLeft.y, PlayerZoneUpperRight.y);
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
    public GameObject[] MakeRandomUrn()
    {
        var randomUrn = new GameObject[CalculateUrnSize()];
        int i = 0;
        foreach (RandomSpawn spawn in spawns)
        {
            for (int j = 0; j < spawn.amount; j++)
            {
                randomUrn[i++] = spawn.prefab;
            }
        }
        return randomUrn;
    }

    private int CalculateUrnSize()
    {
        int count = 0;
        foreach (RandomSpawn spawn in spawns)
        {
            count += spawn.amount;
        }
        return count;
    }

    [Serializable]public struct RandomSpawn
    {
        public GameObject prefab;
        public int amount;
    }
    
}
