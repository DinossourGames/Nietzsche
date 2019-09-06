using System;
using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector2 _size;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _size = _spriteRenderer.bounds.extents;
    }

    private void Update()
    {
        BallTouch();
    }

    private void BallTouch()
    {
        foreach (var touch in Input.touches)
        {
            Vector2 poz = Camera.main.ScreenToWorldPoint(touch.position);
            var origin = transform.position;

            if (!(poz.x > origin.x - _size.x) || !(poz.x < origin.x + _size.x) ||
                !(poz.y > origin.y - _size.y) || !(poz.y < origin.y + _size.y)) continue;
            
            var x = poz.x - origin.x;
            var y = poz.y - origin.y;
            var direction = new Vector2(x, y);

            if (touch.phase == TouchPhase.Ended)
            {
                _rigidbody2D.velocity = -direction * speed * Time.deltaTime;
            }
        }
    }
}