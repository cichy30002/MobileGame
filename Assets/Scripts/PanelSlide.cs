using UnityEngine;

public class PanelSlide : MonoBehaviour
{
    private RectTransform _rectTransform;
    private void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void ShowFromRight(float delay)
    {
        Invoke(nameof(TweenInX),delay);
        
    }
    private void TweenInX()
    {
        LeanTween.moveX(_rectTransform, 0f, 1f).setEaseOutElastic();
    }
    public void CloseToRight()
    {
        LeanTween.moveX(_rectTransform, 1000f, 0.7f).setEaseInOutBack();
    }
    public void ShowFromBottom(float delay)
    {
        Invoke(nameof(TweenInY),delay);
        
    }
    private void TweenInY()
    {
        LeanTween.moveY(_rectTransform, 0f, 1f).setEaseOutElastic();
    }
    public void CloseToBottom()
    {
        LeanTween.moveY(_rectTransform, -1000f, 0.7f).setEaseInOutBack();
    }
}
