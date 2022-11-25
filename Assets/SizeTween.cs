using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SizeTween : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float maxSize = 25f;
    [SerializeField] private float minSize = 20f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private bool loop = false;
    [SerializeField] private bool startOnAwake = false;

    private void Awake()
    {
        if (!startOnAwake) return;
        if (loop)
        {
            InvokeRepeating(nameof(Tween), 0f,2*duration);
        }
        else
        {
            Tween();
        }
    }

    public void Tween()
    {
        LeanTween.value(minSize, maxSize, duration)
            .setEaseInQuad()
            .setOnUpdate(SetFontSize);
        LeanTween.value(maxSize, minSize, duration)
            .setEaseOutQuad()
            .setDelay(duration)
            .setOnUpdate(SetFontSize);
    }

    private void SetFontSize(float value)
    {
        text.fontSize = value;
    }
}
