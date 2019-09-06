using System;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    public Vector2 screenBounds;
    private float _objectWidth;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed = 10f;

    private void Start()
    {
        _objectWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        screenBounds =
            mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    private void LateUpdate()
    {
        Repulsion();
        var viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + _objectWidth, screenBounds.x - _objectWidth);
        transform.position = viewPos;
    }

    private void Repulsion()
    {
        if (!(transform.position.x + _objectWidth > screenBounds.x) && !(transform.position.x - _objectWidth < screenBounds.x * -1 )) return;
        
        var dir = _rigidbody2D.velocity * -1;
        _rigidbody2D.velocity = dir * 2;
    }
}