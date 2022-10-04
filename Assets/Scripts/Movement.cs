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
    public float baseSpeed = 50f;
    public float boostCooldown = 3f;
    public float boostTime = 1f;
    public float boostPower = 0.5f;
    private float _speed = 50f;
    private float _cooldownTime = 0f;

    private void Start()
    {
        ResetSpeed();
    }

    void FixedUpdate()
    {
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
        if (boostTime >= boostCooldown) Debug.LogError("boostTime >= boostCooldown!");
        if (Time.time < _cooldownTime) return;
        _cooldownTime = Time.time + boostCooldown;
        _speed = baseSpeed * (1f + boostPower);
        Invoke(nameof(ResetSpeed), boostTime);
    }

    private void ResetSpeed()
    {
        _speed = baseSpeed;
    }
    
}
