using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UpgradeManager upgradeManager;
    public GameObject gameOver;
    public UnityEvent startGame;
    
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
        
        startGame.Invoke();
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitApp()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        Save();
        gameOver.SetActive(true);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    

    private void Save()
    {
        PlayerPrefs.SetInt("highScore", Math.Max(upgradeManager.stats.HighScore, upgradeManager.stats.Points));
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
            upgradeManager.stats.HighScore = 0;
            upgradeManager.GetUpgradeable("Speed").currentLevel = 0;
            upgradeManager.GetUpgradeable("Boost Power").currentLevel = 0;
            upgradeManager.GetUpgradeable("Boost Time").currentLevel = 0;
            upgradeManager.GetUpgradeable("Tank Size").currentLevel = 0;
            upgradeManager.GetUpgradeable("Health").currentLevel = 0;
            upgradeManager.GetUpgradeable("Radar").currentLevel = 0;
            return;
        }
        upgradeManager.stats.SpareParts = PlayerPrefs.GetInt("spareParts");
        upgradeManager.stats.HighScore = PlayerPrefs.GetInt("highScore");
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
