using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stats : MonoBehaviour
{
    public float fuelCons = 3f;
    public float fuelConsIdle = 1f;
    public float baseSpeed = 50f;
    public float idleSpeed = 25f;
    public float boostCooldown = 3f;
    public float boostTime = 1f;
    public float boostPower = 0.5f;
    
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private MovementEffects movementEffects;
    [SerializeField] private Slider fuelBar;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text sparePartsCounter;
    [SerializeField] private TMP_Text sparePartsCounterUpgrades;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text alertText;
    [SerializeField] private TMP_Text alertIconFuel;
    [SerializeField] private TMP_Text alertIconHealth;
    
    private float _hp;
    private float _maxHp = 100f;
    private float _fuel;
    private float _maxFuel = 100f;
    private int _points;
    private int _spareParts;
    private int _highScore;
    private bool _gameOver;
    private bool _gameStarted;
    private GameManager _gm;
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
                movementEffects.ExplosionVFX();
            }
            UpdateAlerts();
        }
    }
    public float Fuel
    {
        get => _fuel;
        set
        {
            _fuel = value;
            fuelBar.value = _fuel;
            if (_fuel <= 0f)
            {
                GameOver();
                movementEffects.NoFuelVFX();
            }
            UpdateAlerts();
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
            sparePartsCounterUpgrades.text = _spareParts.ToString();
        }
    }
    public int HighScore
    {
        get => _highScore;
        set
        {
            _highScore = value;
            highScoreText.text = "High score: " + _highScore + "!";
            highScoreText.gameObject.SetActive(_highScore!=0 && !_gameStarted);
        }
    }

    private void Start()
    {
        _gameStarted = false;
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gm.startGame.AddListener(StartGame);
        SetStats();
    }

    private void StartGame()
    {
        _gameStarted = true;
        SetStats();
        highScoreText.gameObject.SetActive(false);
    }
    private void SetStats()
    {
        _maxFuel = upgradeManager.GetUpgradeable("Tank Size").Value();
        _maxHp = upgradeManager.GetUpgradeable("Health").Value();
        baseSpeed = upgradeManager.GetUpgradeable("Speed").Value();
        boostPower = upgradeManager.GetUpgradeable("Boost Power").Value(); 
        boostTime = upgradeManager.GetUpgradeable("Boost Time").Value();
        boostCooldown = boostTime * 3f;
        healthBar.maxValue = _maxHp;
        fuelBar.maxValue = _maxFuel;
        Fuel = _maxFuel;
        Hp = _maxHp;
        Points = 0;
        _gameOver = false;
    }

    private void GameOver()
    {
        if (_gameOver) return;
        _gameOver = true;
        _gm.GameOver();
    }

    public void FillFuelTank()
    {
        Fuel = _maxFuel;
    }

    private void UpdateAlerts()
    {
        var alertMessage = "";
        if (Fuel <= _maxFuel * 0.2f) alertMessage += "Critical Fuel Level ";
        if (Hp <= _maxHp * 0.2f) alertMessage += "Critical Health Level";
        alertText.text = alertMessage;
        
        alertIconFuel.gameObject.SetActive(Fuel <= _maxFuel * 0.2f);
        alertIconHealth.gameObject.SetActive(Hp <= _maxHp * 0.2f);
    }
}
