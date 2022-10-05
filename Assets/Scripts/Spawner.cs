using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Position pos;
    public StartSpawner startSpawner;
    public LayerMask layerMask;
    public int minObjects = 10;
    
    private Vector2 _lowerLeft;
    private Vector2 _upperRight;
    private const float Height = 10f;
    private const float Width = 10f;
    private Vector2 _center;
    private Vector2 _size;
    private Collider2D[] _hitObjects;
    private GameObject[] _randomUrn;
    private void Start()
    {
        InvokeRepeating(nameof(CheckSpawn),0.1f,0.5f);
        MakeRandomUrn();
    }

    private void CheckSpawn()
    {
        UpdatePosition();
        if(!EnoughObjects())
        {
            SpawnNewObjects();
        }
    }
    private void UpdatePosition()
    {
        switch (pos)
        {
            case Position.Top:
                _lowerLeft.x = startSpawner.playerZoneLowerLeft.x;
                _lowerLeft.y = startSpawner.playerZoneUpperRight.y;
                _upperRight.x = startSpawner.playerZoneUpperRight.x;
                _upperRight.y = startSpawner.playerZoneUpperRight.y + Height;
                break;
            case Position.Bot:
                _lowerLeft.x = startSpawner.playerZoneLowerLeft.x;
                _lowerLeft.y = startSpawner.playerZoneLowerLeft.y - Height;
                _upperRight.x = startSpawner.playerZoneUpperRight.x;
                _upperRight.y = startSpawner.playerZoneLowerLeft.y;
                break;
            case Position.Left:
                _lowerLeft.x = startSpawner.playerZoneLowerLeft.x - Width;
                _lowerLeft.y = startSpawner.playerZoneLowerLeft.y - Height;
                _upperRight.x = startSpawner.playerZoneLowerLeft.x;
                _upperRight.y = startSpawner.playerZoneUpperRight.y + Height;
                break;
            case Position.Right:
                _lowerLeft.x = startSpawner.playerZoneUpperRight.x;
                _lowerLeft.y = startSpawner.playerZoneLowerLeft.y - Height;
                _upperRight.x = startSpawner.playerZoneUpperRight.x + Width;
                _upperRight.y = startSpawner.playerZoneUpperRight.y + Height;
                break;
            default:
                break;
        }

        _size = new Vector2(_upperRight.x - _lowerLeft.x, _upperRight.y - _lowerLeft.y);
        _center = (_lowerLeft + _upperRight) / 2;
    }

    private bool EnoughObjects()
    {
        _hitObjects = Physics2D.OverlapBoxAll(_center, _size ,0f, layerMask);
        return _hitObjects.Length >= minObjects;
    }

    private void SpawnNewObjects()
    {
        for(int i=_hitObjects.Length;i <= minObjects*1.3f;i++)
        {
            Instantiate(RandomObject(), RandomPositionInZone(), Quaternion.identity);
        }
    }

    private void MakeRandomUrn()
    {
        int count = 0;
        foreach (StartSpawner.RandomSpawn spawn in startSpawner.spawns)
        {
            count += spawn.amount;
        }
        _randomUrn = new GameObject[count];
        int i = 0;
        foreach (StartSpawner.RandomSpawn spawn in startSpawner.spawns)
        {
            for (int j = 0; j < spawn.amount; j++)
            {
                _randomUrn[i++] = spawn.prefab;
            }
        }
    }
    private GameObject RandomObject()
    {
        return _randomUrn[Random.Range(0, _randomUrn.Length)];
    }

    private Vector2 RandomPositionInZone()
    {
        return new Vector2(Random.Range(_lowerLeft.x, _upperRight.x), Random.Range(_lowerLeft.y, _upperRight.y));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 1);
        Gizmos.DrawWireCube(_center, _size);
    }

    public enum Position
    {Top,
    Bot,
    Left,
    Right
};
}
