using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public float startFuel = 100f;
    public float maxFuel = 100f;
    public float fuelCons = 1f;
    public float startHp = 100f;
    public float maxHp = 100f;
    public Slider fuelBar;
    public Slider healthBar;
    public TMP_Text scoreText;
    public TMP_Text sparePartsCounter;

    private float _hp;
    private float _fuel;
    private int _points;
    private int _spareParts;
    public float Hp
    {
        get => _hp;
        set { _hp = value;
            healthBar.value = _hp;
        }
    }
    public float Fuel
    {
        get => _fuel;
        set
        {
            _fuel = value;
            fuelBar.value = _fuel;
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
    private void Start()
    {
        healthBar.maxValue = maxHp;
        fuelBar.maxValue = maxFuel;
        Fuel = startFuel;
        Hp = startHp;
        Points = 0;
        SpareParts = 0;

    }
    
}
