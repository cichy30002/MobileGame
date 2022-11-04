using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlide : MonoBehaviour
{
    public void Show(float delay)
    {
        Invoke(nameof(TweenIn),delay);
        
    }
    private void TweenIn()
    {
        LeanTween.moveX(gameObject, 1200f, 1f).setEaseOutElastic();
    }
    public void Close()
    {
        LeanTween.moveX(gameObject, 5000f, 0.7f).setEaseInOutBack();
    }
}
