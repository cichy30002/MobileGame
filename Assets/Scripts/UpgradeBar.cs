using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBar : MonoBehaviour
{
    [SerializeField] private string statName;
    [SerializeField] private GameObject upgradeTile;
    [SerializeField] private GameObject tiles;
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Button arrow;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private Stats stats;

    private UpgradeManager.Upgradeable _upgradeable;
    private bool _maxed;
    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        _maxed = false;
        _upgradeable = upgradeManager.GetUpgradeable(statName);
        nameText.text = statName;
        arrow.onClick.AddListener(IsMaxed);
        MakeTiles();
    }

    private void MakeTiles()
    {
        for (int i = 0; i < _upgradeable.maxLevel; i++)
        {
            Instantiate(upgradeTile, Vector3.zero, Quaternion.identity, tiles.transform);
        }
    }
    private void Update()
    {
        if (_maxed) return;
        SetVisuals();
    }

    private void SetVisuals()
    {
        priceText.text = _upgradeable.currentPrice.ToString();
        slider.value = (float)_upgradeable.currentLevel / (float)_upgradeable.maxLevel;
        arrow.interactable = stats.SpareParts >= _upgradeable.currentPrice;
    }

    private void IsMaxed()
    {
        if (_maxed) return;
        if (_upgradeable.currentLevel == _upgradeable.maxLevel)
        {
            _maxed = true;
            priceText.text = "MAX";
            arrow.interactable = false;
        }
    }
}
