using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class MovementEffects : MonoBehaviour
{
    [SerializeField] private float launchSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private ParticleSystem boomParticle;
    [SerializeField] private ParticleSystem noFuelParticle;
    [SerializeField] private float boostEmissionMultiplier = 1.5f;
    [SerializeField] private TMP_Text launchText;
    
    private ParticleSystem.EmissionModule _emission;
    private GameManager _gm;
    private float _startEmission;
    private void Start()
    {
        _emission = fireParticle.emission;
        _startEmission = _emission.rateOverTime.constant;
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gm.startGame.AddListener(StartGame);
    }
    private void StartGame()
    {
        StartCoroutine(nameof(LaunchEffect));
    }

    public void SetBoostFX(bool switchOn)
    {
        if (switchOn)
        {
            _emission.rateOverTime = _startEmission*boostEmissionMultiplier;
        }
        else
        {
            _emission.rateOverTime = _startEmission;
        }
    }
    private IEnumerator LaunchEffect()
    {
        launchText.gameObject.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            launchText.text = (3-i).ToString();
            yield return new WaitForSeconds(0.7f);
        }
        launchText.gameObject.SetActive(false);
        float accelerationTime = 50;
        for (float i = 0; i < accelerationTime/2; i++)
        {
            rb.velocity = Vector2.Lerp(Vector2.zero, Vector2.up,i/accelerationTime) * launchSpeed;
            yield return null;
        }
        _gm.StartPlaying();
        for (float i = accelerationTime/2; i < accelerationTime; i++)
        {
            rb.velocity = Vector2.Lerp(Vector2.zero, Vector2.up,i/accelerationTime) * launchSpeed;
            yield return null;
        }
    }
    public void ExplosionVFX()
    {
        Camera.main.GetComponent<CameraMovement>().Shake(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        fireParticle.Stop();
        Instantiate(boomParticle, transform.position, Quaternion.identity);
        //play sound
    }

    public void NoFuelVFX()
    {
        Instantiate(noFuelParticle,fireParticle.transform.position, quaternion.identity, gameObject.transform);
        fireParticle.Stop();
        //play sound
    }
}
