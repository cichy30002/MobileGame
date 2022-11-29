using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Position pos;
    [SerializeField] private StartSpawner startSpawner;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int minObjects = 10;
    
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
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.startGame.AddListener(StartGame);
    }
    private void StartGame()
    {
        InvokeRepeating(nameof(CheckSpawn),0.1f,0.5f);
        _randomUrn = startSpawner.MakeRandomUrn();
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
                _lowerLeft.x = startSpawner.PlayerZoneLowerLeft.x;
                _lowerLeft.y = startSpawner.PlayerZoneUpperRight.y;
                _upperRight.x = startSpawner.PlayerZoneUpperRight.x;
                _upperRight.y = startSpawner.PlayerZoneUpperRight.y + Height;
                break;
            case Position.Bot:
                _lowerLeft.x = startSpawner.PlayerZoneLowerLeft.x;
                _lowerLeft.y = startSpawner.PlayerZoneLowerLeft.y - Height;
                _upperRight.x = startSpawner.PlayerZoneUpperRight.x;
                _upperRight.y = startSpawner.PlayerZoneLowerLeft.y;
                break;
            case Position.Left:
                _lowerLeft.x = startSpawner.PlayerZoneLowerLeft.x - Width;
                _lowerLeft.y = startSpawner.PlayerZoneLowerLeft.y - Height;
                _upperRight.x = startSpawner.PlayerZoneLowerLeft.x;
                _upperRight.y = startSpawner.PlayerZoneUpperRight.y + Height;
                break;
            case Position.Right:
                _lowerLeft.x = startSpawner.PlayerZoneUpperRight.x;
                _lowerLeft.y = startSpawner.PlayerZoneLowerLeft.y - Height;
                _upperRight.x = startSpawner.PlayerZoneUpperRight.x + Width;
                _upperRight.y = startSpawner.PlayerZoneUpperRight.y + Height;
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

    private enum Position
    {Top,
    Bot,
    Left,
    Right
};
}
