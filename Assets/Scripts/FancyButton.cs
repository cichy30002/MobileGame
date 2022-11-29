using UnityEngine;

public class FancyButton : MonoBehaviour
{
    [SerializeField] private float delay = 0f;
    
    private RectTransform _rectTransform;
    private float _startX;
    private void Awake()
    {
         _rectTransform= gameObject.GetComponent<RectTransform>();
        _rectTransform.localScale = Vector3.zero;
        Invoke(nameof(TweenSize), delay);
    }

    private void TweenSize()
    {
        LeanTween.scale(_rectTransform, Vector3.one, 1f).setEaseOutElastic();
        _startX = _rectTransform.anchoredPosition.x;
    }

    public void SlideOut(float value)
    {
        LeanTween.moveX(_rectTransform, value, 1f).setEaseInOutBack();
    }

    public void SlideIn(float delay)
    {
        Invoke(nameof(TweenIn),delay);
    }

    private void TweenIn()
    {
        LeanTween.moveX(_rectTransform, _startX, 1f).setEaseOutElastic();
    }
}
