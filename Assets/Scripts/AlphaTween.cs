using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class AlphaTween : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    [SerializeField] private float delay = 0f;

    private CanvasGroup _canvasGroup;
    private void Awake()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
        Invoke(nameof(TweenAlpha), delay);
    }

    private void TweenAlpha()
    {
        LeanTween.alphaCanvas(_canvasGroup, 1f, time).setEaseOutSine();
    }
}
