 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    [SerializeField] private MobileJoystickController controller;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Stats stats;
    [SerializeField] private ParticleSystem fireParticle;
    [SerializeField] private float emissionMultiplier = 1.5f;
    
    
    private float _speed = 50f;
    private float _idleSpeed = 10f;
    private float _cooldownTime = 0f;
    private ParticleSystem.EmissionModule _emission;
    private float _startEmission;
    private bool _idle;

    private void Start()
    {
        _emission = fireParticle.emission;
        _startEmission = _emission.rateOverTime.constant;
        _idle = true;

        Invoke(nameof(ResetSpeed),0.05f);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (stats.Fuel <= 0f) return;
        SetVelocity();
        SetRotation();
        SetFuelCons();
    }

    private void SetVelocity()
    {
        Vector2 newVelocity = new(controller.horizontalAxis, controller.verticalAxis);
        newVelocity *= Time.deltaTime;
        newVelocity *= _speed;
        _idle = newVelocity.magnitude <= 1f;
        if (_idle)
        {
            newVelocity = rb.velocity.normalized * (Time.deltaTime * _idleSpeed);
        }
        // weighted average to make smooth acceleration / deceleration
        rb.velocity = (newVelocity + rb.velocity*10)/11;
    }

    private void SetRotation()
    {
        transform.up = rb.velocity;
    }

    private void SetFuelCons()
    {
        if (_idle)
        {
            stats.Fuel -= stats.fuelConsIdle * Time.deltaTime;
        }
        else
        {
            stats.Fuel -= stats.fuelCons * Time.deltaTime;
        }
    }

    public void Boost()
    {
        if (stats.boostTime >= stats.boostCooldown) Debug.LogError("boostTime >= boostCooldown!");
        if (Time.time < _cooldownTime) return;
        //boost stats
        _cooldownTime = Time.time + stats.boostCooldown;
        _speed = stats.baseSpeed * (1f + stats.boostPower);
        //boost fx
        _emission.rateOverTime = _startEmission*emissionMultiplier;

        Invoke(nameof(ResetSpeed), stats.boostTime);
    }

    private void ResetSpeed()
    {
        //reset stats
        _speed = stats.baseSpeed;
        _idleSpeed = stats.idleSpeed;
        //reset fx
        _emission.rateOverTime = _startEmission;
    }
    
}
