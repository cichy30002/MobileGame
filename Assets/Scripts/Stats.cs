using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    public float maxFuel = 100f;
    public float fuelCons = 1f;
    public float maxHp = 100f;
    public float baseSpeed = 50f;
    public float boostCooldown = 3f;
    public float boostTime = 1f;
    public float boostPower = 0.5f;
    public float radarRange = 20f;
    public LayerMask layerMask;
    public Slider fuelBar;
    public Slider healthBar;
    public TMP_Text scoreText;
    public TMP_Text sparePartsCounter;
    public SpriteRenderer fuelPointerSpriteRenderer;
    [HideInInspector] public int highScore;
    
    private float _hp;
    private float _fuel;
    private int _points;
    private int _spareParts;
    private bool _gameOver;
    public float Hp
    {
        get => _hp;
        set 
        {
            _hp = value;
            healthBar.value = _hp;
            if (_hp <= 0f)
            {
                GameOver();
                ExplosionVFX();
            }
        }
    }
    public float Fuel
    {
        get => _fuel;
        set
        {
            _fuel = value;
            fuelBar.value = _fuel;
            if(_fuel <= 0f) GameOver();
        } 
    }
    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            scoreText.text = _points.ToString();
        } 
    }

    public int SpareParts
    {
        get => _spareParts;
        set
        {
            _spareParts = value;
            sparePartsCounter.text = _spareParts.ToString();
        }
    }
    public void SetStats()
    {
        maxFuel = upgradeManager.GetUpgradeable("Tank Size").Value();
        maxHp = upgradeManager.GetUpgradeable("Health").Value();
        baseSpeed = upgradeManager.GetUpgradeable("Speed").Value();
        boostPower = upgradeManager.GetUpgradeable("Boost Power").Value(); 
        boostTime = upgradeManager.GetUpgradeable("Boost Time").Value();
        boostCooldown = boostTime * 3f;
        healthBar.maxValue = maxHp;
        fuelBar.maxValue = maxFuel;
        Fuel = maxFuel;
        Hp = maxHp;
        Points = 0;
        _gameOver = false;
        fuelPointerSpriteRenderer.gameObject.SetActive(upgradeManager.GetUpgradeable("Radar").currentLevel >= 1);
    }

    private void GameOver()
    {
        if (_gameOver) return;
        _gameOver = true;
        FindObjectOfType<GameManager>().GameOver();
    }

    public void SetGameOver(bool value)
    {
        _gameOver = value;
    }
    private void ExplosionVFX()
    {
        Debug.Log("boom");
    }

    private void Update()
    {
        SetFuelPointer();
    }

    private void SetFuelPointer()
    {
        Color pointerColor;
        Transform barrel = FindClosestBarrel();
        if (barrel == default)
        {
            pointerColor = fuelPointerSpriteRenderer.color;
            pointerColor.a = 0f;
            fuelPointerSpriteRenderer.color = pointerColor;
            return;
        }
        Vector2 dir = barrel.position - transform.position;
        pointerColor = fuelPointerSpriteRenderer.color;
        pointerColor.a = 1f - 2f / dir.magnitude;
        fuelPointerSpriteRenderer.color = pointerColor;
        dir.Normalize();
        Transform pointerTransport = fuelPointerSpriteRenderer.transform;
        pointerTransport.position = new Vector2(transform.position.x + (dir.x), transform.position.y + (dir.y));
        pointerTransport.up = barrel.position - pointerTransport.position;
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
