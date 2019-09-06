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
        foreach (var touch in Input.touches)
        {
            Vector2 poz = Camera.main.ScreenToWorldPoint(touch.position);

            if (poz.x > transform.position.x - _size.x && poz.x < transform.position.x + _size.x &&
                poz.y > transform.position.y - _size.y && poz.y < transform.position.y + _size.y)
            {
                if (touch.phase == TouchPhase.Ended)
                    Debug.Log("Hit");
            }
        }
    }
}