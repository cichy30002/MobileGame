using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Stats stats;
    public List<Upgradeable> upgradeables;

    private void Start()
    {
        CalculatePrices();

        Time.timeScale = 0f;
    }

    public void CalculatePrices()
    {
        foreach (var upgradeable in upgradeables)
        {
            upgradeable.currentPrice = upgradeable.startPrice*(upgradeable.currentLevel + 1);
        }
    }
    public Upgradeable GetUpgradeable(string statName)
    {
        return upgradeables.FirstOrDefault(upgradeable => upgradeable.statName == statName);
    }
    public void LevelUp(string statName)
    {
        Upgradeable upgradeable = GetUpgradeable(statName);
        stats.SpareParts -= upgradeable.currentPrice;
        upgradeable.currentLevel += 1;
        upgradeable.currentPrice = upgradeable.startPrice*(upgradeable.currentLevel + 1);
    }
    [System.Serializable]
    public class Upgradeable
    {
        public string statName;
        public int maxLevel;
        public int currentLevel;
        public int startPrice;
        [HideInInspector]public int currentPrice;
        public float zeroLevelValue;
        public float maxLevelValue;
        public float Value()
        {
            return zeroLevelValue + (maxLevelValue - zeroLevelValue) * ((float)currentLevel/maxLevel);
        }
    }
}
