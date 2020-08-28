using Assets.Scripts.Model;
using UnityEngine;


public sealed class Bullet : Ammunition
{
    [SerializeField] private float _damage = 10.0f;

    public float Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }

    //public void AddForce(Vector3 direction)
    //{
    //    if (!_bullet)
    //    {
    //        return;
    //    }
    //    _bullet.AddForce(direction);
    //}

    public void Velocity(float speed)
    {
        Rigidbody.velocity = new Vector3(0, 0, speed);
    }
}