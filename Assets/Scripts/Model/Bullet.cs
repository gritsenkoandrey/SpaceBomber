using UnityEngine;


public sealed class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    private Rigidbody _bullet;

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

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