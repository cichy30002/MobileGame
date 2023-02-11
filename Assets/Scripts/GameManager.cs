using System;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private GameObject gameOver;

    public UnityEvent startPlaying;
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

    public void StartPlaying()
    {
        startPlaying.Invoke();
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
        PlayerPrefs.SetInt("highScore", Math.Max(_stats.HighScore, _stats.Points));
        PlayerPrefs.SetInt("spareParts", _stats.SpareParts);
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
        if (PlayerPrefs.HasKey("spareParts"))
        {
            LoadSavedStats();
        }
        else
        {
            LoadDefaultStarts();
        }
    }

    private void LoadDefaultStarts()
    {
        _stats.SpareParts = 0;
        _stats.HighScore = 0;
        upgradeManager.GetUpgradeable("Speed").currentLevel = 0;
        upgradeManager.GetUpgradeable("Boost Power").currentLevel = 0;
        upgradeManager.GetUpgradeable("Boost Time").currentLevel = 0;
        upgradeManager.GetUpgradeable("Tank Size").currentLevel = 0;
        upgradeManager.GetUpgradeable("Health").currentLevel = 0;
        upgradeManager.GetUpgradeable("Radar").currentLevel = 0;
    }

    private void LoadSavedStats()
    {
        _stats.SpareParts = PlayerPrefs.GetInt("spareParts");
        _stats.HighScore = PlayerPrefs.GetInt("highScore");
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
