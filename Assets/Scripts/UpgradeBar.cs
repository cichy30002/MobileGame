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

    private UpgradeManager.Upgradable _upgradable;
    private bool _maxed;
    private void Start()
    {
        _maxed = false;
        _upgradable = upgradeManager.GetUpgradable(statName);
        priceText.text = _upgradable.startPrice.ToString();
        nameText.text = statName;
        for (int i = 0; i < _upgradable.maxLevel; i++)
        {
            Instantiate(upgradeTile, Vector3.zero, Quaternion.identity, tiles.transform);
        }
        slider.value = (float)_upgradable.currentLevel / (float)_upgradable.maxLevel;
        arrow.onClick.AddListener(UpdateBar);
    }

    private void Update()
    {
        if (_maxed) return;
        arrow.interactable = stats.SpareParts >= _upgradable.currentPrice;
    }

    private void UpdateBar()
    {
        if (_maxed) return;
        _upgradable = upgradeManager.GetUpgradable(statName);
        slider.value = (float)_upgradable.currentLevel / (float)_upgradable.maxLevel;
        priceText.text = _upgradable.currentPrice.ToString();
        if (_upgradable.currentLevel == _upgradable.maxLevel)
        {
            _maxed = true;
            priceText.text = "MAX";
            arrow.interactable = false;
        }
    }
}
