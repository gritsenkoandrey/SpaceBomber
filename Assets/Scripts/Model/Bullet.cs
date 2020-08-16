using Assets.Scripts.Model;
using UnityEngine;


public sealed class Bullet : Ammunition
{
    [SerializeField] private int _damage = 10;

    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
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
        rigidbody.velocity = new Vector3(0, 0, speed);
    }
}