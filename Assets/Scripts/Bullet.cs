using UnityEngine;


public sealed class Bullet : MonoBehaviour
{
    private Rigidbody _bullet;

    private void Awake()
    {
        _bullet = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector3 direction)
    {
        if (!_bullet)
        {
            return;
        }
        _bullet.AddForce(direction);
    }
}