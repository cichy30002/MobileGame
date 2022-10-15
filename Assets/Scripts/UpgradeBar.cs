using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    public string statName;
    public GameObject upgradeTile;
    public GameObject tiles;
    public Slider slider;
    public TMP_Text priceText;
    public TMP_Text nameText;
    public Button arrow;
    public UpgradeManager upgradeManager;
    public Stats stats;

    private UpgradeManager.Upgradeable _upgradeable;
    private bool _maxed;
    private void Start()
    {
        _maxed = false;
        _upgradeable = upgradeManager.GetUpgradeable(statName);
        nameText.text = statName;
        for (int i = 0; i < _upgradeable.maxLevel; i++)
        {
            Instantiate(upgradeTile, Vector3.zero, Quaternion.identity, tiles.transform);
        }
        arrow.onClick.AddListener(UpdateBar);
    }

    private void Update()
    {
        if (_maxed) return;
        priceText.text = _upgradeable.currentPrice.ToString();
        slider.value = (float)_upgradeable.currentLevel / (float)_upgradeable.maxLevel;
        arrow.interactable = stats.SpareParts >= _upgradeable.currentPrice;
    }

    private void UpdateBar()
    {
        if (_maxed) return;
        _upgradeable = upgradeManager.GetUpgradeable(statName);
        slider.value = (float)_upgradeable.currentLevel / (float)_upgradeable.maxLevel;
        priceText.text = _upgradeable.currentPrice.ToString();
        if (_upgradeable.currentLevel == _upgradeable.maxLevel)
        {
            _maxed = true;
            priceText.text = "MAX";
            arrow.interactable = false;
        }
    }
}
