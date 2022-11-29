using System;
using UnityEngine;

public class MobileJoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform joy;
    [SerializeField] private RectTransform stick;
    [SerializeField] private float minDistanceFromMiddle;

    [HideInInspector] public float horizontalAxis;
    [HideInInspector] public float verticalAxis;
    
    private int _activeTouchID;

    private void Start()
    {
        _activeTouchID = -1;
    }

    private void Update()
    {
        PerformTouches();
    }

    private void PerformTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == _activeTouchID)
            {
                PerformTouches();
            }
            else if (NewControlTouch(touch))
            {
                _activeTouchID = touch.fingerId;
            }
        }
    }

    private void PreformTouchPhases(Touch touch)
    {
        switch (touch.phase)
        {
            case TouchPhase.Moved:
                MoveStick(touch.position);
                UpdateOutput(touch.position);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                _activeTouchID = -1;
                stick.anchoredPosition = Vector3.zero;
                verticalAxis = 0f;
                horizontalAxis = 0f;
                break;
        }
    }
    private bool NewControlTouch(Touch touch)
    {
        return touch.phase == TouchPhase.Began && CloseToMiddle(touch) && _activeTouchID == -1;
    }

    private void UpdateOutput(Vector2 position)
    {
        Vector2 dir = VectorToMiddle(position).normalized;
        horizontalAxis = dir.x;
        verticalAxis = dir.y;
    }


    private void MoveStick(Vector2 position)
    {
        Vector2 vecToMid = VectorToMiddle(position);
        Vector2 dir = vecToMid.normalized;
        float mag = Math.Min(vecToMid.magnitude, joy.rect.height/2.5f);
        stick.anchoredPosition = new Vector3(dir.x * mag, dir.y * mag, 0f);
    }

    private Vector2 VectorToMiddle(Vector2 touchPosition)
    {
        Vector3 joyPosition = joy.position;
        return new Vector2( touchPosition.x - joyPosition.x, touchPosition.y - joyPosition.y);
    }

    private bool CloseToMiddle(Touch touch)
    {
        return VectorToMiddle(touch.position).magnitude < minDistanceFromMiddle;
    }
}
