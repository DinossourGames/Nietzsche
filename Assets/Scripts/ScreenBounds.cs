using System;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public Vector2 screenBounds;
    private float _objectWidth;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    private void LateUpdate()
    {
        Repulsion();
        var viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x,
            screenBounds.x * -1 + _objectWidth,
            screenBounds.x - _objectWidth);
        transform.position = viewPos;
    }

    private void Repulsion()
    {
        if (!(transform.position.x + _objectWidth > screenBounds.x) &&
            !(transform.position.x - _objectWidth < screenBounds.x * -1)) return;

        var dir = _rigidbody2D.velocity * -1;
        if (Player.HasTouched)
        {
            _rigidbody2D.velocity = dir * 1.6f;
            Player.HasTouched = false;
        }
        else
        {
            _rigidbody2D.velocity = dir * .6f;
            Player.HasTouched = false;
        }
    }
}