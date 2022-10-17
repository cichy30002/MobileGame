using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    public float gameOverVFXTime = 3f;
    public Image gameOverPanel;
    public TMP_Text gameOverText;
    private Stats _stats;
    private void Start()
    {
        _stats = FindObjectOfType<Stats>();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            Load();
            upgradeManager.CalculatePrices();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void ResetTimeScale()
    {
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        Save();
        GameOverVFX();
        Invoke(nameof(BackToMenu), gameOverVFXTime);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void GameOverVFX()
    {
        gameOverPanel.gameObject.SetActive(true);
        StartCoroutine(AlphaVFX());
    }

    private void ResetGameOverVFX()
    {
        CancelInvoke(nameof(BackToMenu));
        _stats.SetGameOver(false);
        gameOverPanel.gameObject.SetActive(false);
    }
    private IEnumerator AlphaVFX()
    {
        var ticks = (int)(gameOverVFXTime * 10);
        Color panelColor = gameOverPanel.color;
        Color textColor = gameOverText.color;
        
        for (int i = 0; i < ticks; i++)
        {
            if (_stats.Fuel > 0.1f && _stats.Hp > 0.1f) 
            {
                ResetGameOverVFX();
                break;
            }
            panelColor.a = (float)i / ticks;
            textColor.a = (float)i / ticks;
            gameOverPanel.color = panelColor;
            gameOverText.color = textColor;
            yield return new WaitForSeconds(0.07f);
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("highScore", Math.Max(upgradeManager.stats.highScore, upgradeManager.stats.Points));
        PlayerPrefs.SetInt("spareParts", upgradeManager.stats.SpareParts);
        PlayerPrefs.SetInt("speedLevel", upgradeManager.GetUpgradeable("Speed").currentLevel);
        PlayerPrefs.SetInt("boostPowerLevel", upgradeManager.GetUpgradeable("Boost Power").currentLevel);
        PlayerPrefs.SetInt("boostTimeLevel", upgradeManager.GetUpgradeable("Boost Time").currentLevel);
        PlayerPrefs.SetInt("tankSizeLevel", upgradeManager.GetUpgradeable("Tank Size").currentLevel);
        PlayerPrefs.SetInt("healthLevel", upgradeManager.GetUpgradeable("Health").currentLevel);
        PlayerPrefs.SetInt("radarLevel", upgradeManager.GetUpgradeable("Radar").currentLevel);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey("spareParts"))
        {
            upgradeManager.stats.SpareParts = 0;
            upgradeManager.stats.highScore = 0;
            upgradeManager.GetUpgradeable("Speed").currentLevel = 0;
            upgradeManager.GetUpgradeable("Boost Power").currentLevel = 0;
            upgradeManager.GetUpgradeable("Boost Time").currentLevel = 0;
            upgradeManager.GetUpgradeable("Tank Size").currentLevel = 0;
            upgradeManager.GetUpgradeable("Health").currentLevel = 0;
            upgradeManager.GetUpgradeable("Radar").currentLevel = 0;
            return;
        }
        upgradeManager.stats.SpareParts = PlayerPrefs.GetInt("spareParts");
        upgradeManager.stats.highScore = PlayerPrefs.GetInt("highScore");
        upgradeManager.GetUpgradeable("Speed").currentLevel = PlayerPrefs.GetInt("speedLevel");
        upgradeManager.GetUpgradeable("Boost Power").currentLevel = PlayerPrefs.GetInt("boostPowerLevel");
        upgradeManager.GetUpgradeable("Boost Time").currentLevel = PlayerPrefs.GetInt("boostTimeLevel");
        upgradeManager.GetUpgradeable("Tank Size").currentLevel = PlayerPrefs.GetInt("tankSizeLevel");
        upgradeManager.GetUpgradeable("Health").currentLevel = PlayerPrefs.GetInt("healthLevel");
        upgradeManager.GetUpgradeable("Radar").currentLevel = PlayerPrefs.GetInt("radarLevel");
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();
    }
    
}
