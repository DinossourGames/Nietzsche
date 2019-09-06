using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool isLocked;

    public static bool HasTouched;
    private Vector2 _size;
    private Vector3 origin;
    [SerializeField] private float offset;


    void Start()
    {
        isLocked = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _size = _spriteRenderer.bounds.extents;
    }

    private void Update()
    {
//        if (Mathf.Abs(_rigidbody2D.velocity.x) > speed)
//        {
//            _rigidbody2D.velocity /= 2;
//        }
        origin = transform.position;
    }


    private void FixedUpdate()
    {
        BallTouch();
    }

    private void BallTouch()
    {
        foreach (var touch in Input.touches)
        {
            if (isLocked)
            {
                isLocked = false;
                _rigidbody2D.isKinematic = false;
            }

            Vector2 poz = Camera.main.ScreenToWorldPoint(touch.position);


            if (!(poz.x > origin.x - _size.x - offset) || !(poz.x < origin.x + _size.x + offset) ||
                !(poz.y > origin.y - _size.y - offset) || !(poz.y < origin.y + _size.y + offset)) continue;

//            var x = poz.x - origin.x;
//            var y = poz.y - origin.y;
//            var direction = new Vector2(x, y);

           var direction = Vector3.Normalize((Vector2)origin - poz);

            print(direction);
           
            Debug.DrawLine(origin, poz, Color.red);

            if (touch.phase == TouchPhase.Began)
                _rigidbody2D.AddForce(direction * speed,ForceMode2D.Impulse);
            
            
            HasTouched = true;
        }
    }
}