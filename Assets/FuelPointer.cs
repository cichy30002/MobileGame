using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPointer : MonoBehaviour
{
    [SerializeField] private float radarRange = 20f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SpriteRenderer fuelPointerSpriteRenderer;
    [SerializeField] private UpgradeManager upgradeManager;
    private bool _gameStarted;
    private void Start()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.startGame.AddListener(StartGame);
    }
    private void StartGame()
    {
        _gameStarted = true;
        fuelPointerSpriteRenderer.gameObject.SetActive(upgradeManager.GetUpgradeable("Radar").currentLevel >= 1);
    }
    private void Update()
    {
        if (_gameStarted)
        {
            SetFuelPointer();
        }
    }
    private void SetFuelPointer()
    {
        Transform barrel = FindClosestBarrel();
        if (barrel == default)
        {
            SetPointerAlpha(0f);
            return;
        }
        Vector2 dir = barrel.position - transform.position;
        SetPointerAlpha(1f - 2f / dir.magnitude);
        SetPointerPosition(dir.normalized, barrel);
    }

    private void SetPointerPosition(Vector2 dir, Transform barrel)
    {
        Transform pointerTransport = fuelPointerSpriteRenderer.transform;
        pointerTransport.position = new Vector2(transform.position.x + (dir.x), transform.position.y + (dir.y));
        pointerTransport.up = barrel.position - pointerTransport.position;
    }

    private void SetPointerAlpha(float alpha)
    {
        Color pointerColor = fuelPointerSpriteRenderer.color;
        pointerColor.a = alpha;
        fuelPointerSpriteRenderer.color = pointerColor;
    }

    private Transform FindClosestBarrel()
    {
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, radarRange, layerMask);
        Transform closestBarrelInRange = default;
        float minDistance = 99999f;
        foreach (var objectInRange in objectsInRange)
        {
            if (objectInRange.gameObject.CompareTag("Barrel"))
            {
                float distanceToObject = Vector2.Distance(transform.position, objectInRange.transform.position);
                if (distanceToObject < minDistance)
                {
                    closestBarrelInRange = objectInRange.transform;
                    minDistance = distanceToObject;
                }
            }
        }

        return closestBarrelInRange;
    }
}
