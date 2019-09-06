using System;
using UnityEditor;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 _fingerDownPosition;
    private Vector2 _fingerUpPosition;
    [SerializeField, Range(0f, 200f)] private float sensebilityThreshold = 50;


    public static event Action<SwipeData> OnSwipe = delegate { };

    private void Update()
    {
        foreach (var touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _fingerUpPosition = touch.position;
                    _fingerDownPosition = touch.position;
                    break;
                case TouchPhase.Ended:
                    _fingerDownPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        var dir = Vector2.zero;

        dir.x = _fingerDownPosition.x - _fingerUpPosition.x;
        dir.y = _fingerDownPosition.y - _fingerUpPosition.y;

        SwipeDirection direction;
        Vector2 vectorDirection;

        if (dir.y < sensebilityThreshold && dir.y > -sensebilityThreshold)
        {
            if (dir.x > 0)
            {
                direction = SwipeDirection.Right;
                vectorDirection = Vector2.right;
            }
            else
            {
                direction = SwipeDirection.Left;
                vectorDirection = Vector2.left;
            }
        }
        else
        {
            if (dir.x < sensebilityThreshold && dir.x > -sensebilityThreshold)
            {
                if (dir.y > 0)
                {
                    direction = SwipeDirection.Up;
                    vectorDirection = Vector2.up;
                }
                else
                {
                    direction = SwipeDirection.Down;
                    vectorDirection = Vector2.down;
                }
            }
            else
            {
                if (dir.y > 0)
                {
                    if (dir.x > 0)
                    {
                        direction = SwipeDirection.UpRight;
                        vectorDirection = new Vector2(1, 1);
                    }
                    else
                    {
                        direction = SwipeDirection.UpLeft;
                        vectorDirection = new Vector2(-1, 1);
                    }
                }
                else
                {
                    if (dir.x > 0)
                    {
                        direction = SwipeDirection.DownRight;
                        vectorDirection = new Vector2(1, -1);
                    }
                    else
                    {
                        direction = SwipeDirection.DownLeft;
                        vectorDirection = new Vector2(-1, -1);
                    }
                }
            }
        }

        SendSwipe(direction, vectorDirection);
        _fingerUpPosition = _fingerDownPosition;
    }

    private void SendSwipe(SwipeDirection direction, Vector2 vector2Direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            VectorDirection = vector2Direction,
            StartPosition = _fingerDownPosition,
            EndPosition = _fingerUpPosition
        };
        OnSwipe(swipeData);
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
    public Vector2 VectorDirection;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right,
    UpRight,
    UpLeft,
    DownRight,
    DownLeft
}