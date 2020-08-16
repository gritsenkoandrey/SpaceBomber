﻿using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private int _health = 100;
    private readonly string _explosionShip = "ShipExplosion";

    private Bullet _bullet;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();

        if (_bullet)
        {
            Health -= _bullet.Damage;
            _bullet.GetComponent<PoolObject>().ReturnToPool();
            if (_health <= 0)
            {
                prefab = PoolManager.GetObject(_explosionShip,
                    this.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(ReturnToPool(prefab));
                // todo наш корабль
                Destroy(this.gameObject);
            }
        }
        else
        {
            return;
        }
    }
}