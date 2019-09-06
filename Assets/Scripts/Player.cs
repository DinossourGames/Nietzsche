using System.Runtime.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float speed;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetectorOnOnSwipe;
    }

    private void SwipeDetectorOnOnSwipe(SwipeData obj)
    {
        _rigidbody2D.velocity = obj.VectorDirection * speed;
    }

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
}