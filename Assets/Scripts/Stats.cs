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
    public Slider fuelBar;
    public Slider healthBar;
    public TMP_Text scoreText;
    public TMP_Text sparePartsCounter;
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
    }

    private void GameOver()
    {
        if (_gameOver) return;
        _gameOver = true;
        Debug.Log("game over");
        FindObjectOfType<GameManager>().GameOver();
    }

    private void ExplosionVFX()
    {
        Debug.Log("boom");
    }
}
