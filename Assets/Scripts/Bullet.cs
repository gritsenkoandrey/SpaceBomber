using UnityEngine;


public sealed class Bullet : MonoBehaviour
{
    private Rigidbody _bullet;
    private float _speed = 40.0f;

    private void Start()
    {
        _bullet = GetComponent<Rigidbody>();
        _bullet.velocity = new Vector3(0, 0, _speed);
    }
}