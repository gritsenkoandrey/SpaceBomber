using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private int _health = 100;
    private readonly string _explosionShip = "ShipExplosion";

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)
        {
            Health -= bullet.Damage;
            bullet.GetComponent<PoolObject>().ReturnToPool();
            if (_health <= 0)
            {
                PoolManager.GetObject(_explosionShip, this.gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            return;
        }
    }
}