using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Stats stats;
    public List<Upgradable> upgradeables;

    private void Start()
    {
        foreach (var upgradable in upgradeables)
        {
            upgradable.currentPrice = upgradable.startPrice;
        }

        Time.timeScale = 0f;
    }

    public Upgradable GetUpgradable(string statName)
    {
        return upgradeables.FirstOrDefault(upgradable => upgradable.statName == statName);
    }
    public void LevelUp(string statName)
    {
        Upgradable upgradeable = GetUpgradable(statName);
        stats.SpareParts -= upgradeable.currentPrice;
        upgradeable.currentLevel += 1;
        upgradeable.currentPrice += upgradeable.startPrice;
    }
    [System.Serializable]
    public class Upgradable
    {
        public string statName;
        public int maxLevel;
        public int currentLevel;
        public int startPrice;
        [HideInInspector]public int currentPrice;
    }
}
