using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    public MobileJoystickController controller;
    public Rigidbody2D rb;
    public Stats stats;
    public ParticleSystem fireParticle;
    private float _speed = 50f;
    private float _cooldownTime = 0f;

    private void Start()
    {
        Invoke(nameof(ResetSpeed),0.05f);
    }

    void FixedUpdate()
    {
        if (stats.Fuel <= 0f) return;
        Vector2 newVelocity = new(controller.horizontalAxis, controller.verticalAxis);
        newVelocity *= Time.deltaTime;
        newVelocity *= _speed;
        rb.velocity = newVelocity;
        if (newVelocity.magnitude >= 0.01f)
        {
            transform.up = newVelocity;
            stats.Fuel -= stats.fuelCons * Time.deltaTime;
        }
    }

    public void Boost()
    {
        if (stats.boostTime >= stats.boostCooldown) Debug.LogError("boostTime >= boostCooldown!");
        if (Time.time < _cooldownTime) return;
        
        _cooldownTime = Time.time + stats.boostCooldown;
        _speed = stats.baseSpeed * (1f + stats.boostPower);
        var emission = fireParticle.emission;
        emission.rateOverTime = 80f;
        Invoke(nameof(ResetSpeed), stats.boostTime);
    }

    private void ResetSpeed()
    {
        _speed = stats.baseSpeed;
        var emission = fireParticle.emission;
        emission.rateOverTime = 50f;
    }
    
}
