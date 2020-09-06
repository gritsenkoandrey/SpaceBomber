﻿using Assets.Scripts.Manager;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private float _maxHealth = 100.0f;
    private float _currentHealth;
    private readonly byte _minHealth = 0;
    private readonly float _quarter = 0.25f;
    private readonly int _maxPercent = 100;

    private float _tempValue;
    private readonly byte _shieldDestroyed = 0;

    private readonly string _explosionShip = "ShipExplosion";
    private readonly string _explosionAsteroid = "AsteroidExplosion";
    private readonly string _explosionAsteroidSound = "Grenade3Short";
    private readonly string _explosionShipSound = "Grenade6Short";

    private Bullet _bullet;
    private SpaceshipEnemy _enemyShip;
    private AsteroidModel _asteroid;
    private SpaceshipShield _shield;

    public float CurrentHealth
    {
        get
        {
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
            return _currentHealth;
        }
        private set { _currentHealth = value; }
    }

    public float FillHealth
    {
        get { return _currentHealth / _maxHealth; }
    }

    public float AverageHealth
    {
        get { return _maxHealth * _quarter; }
    }

    public float PercentHealth
    {
        get
        {
            if (_currentHealth <= _minHealth)
            {
                return _minHealth;
            }
            else
            {
                return _currentHealth / _maxHealth * _maxPercent;
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _currentHealth = _maxHealth;
        _shield = GetComponentInChildren<SpaceshipShield>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _enemyShip = other.gameObject.GetComponent<SpaceshipEnemy>();
        _asteroid = other.gameObject.GetComponent<AsteroidModel>();

        if (_bullet || _enemyShip || _asteroid)
        {
            if (_bullet)
            {
                DamageTaken(_bullet.Damage);
                _bullet.GetComponent<PoolObject>().ReturnToPool();
            }
            else if (_enemyShip)
            {
                DamageTaken(_enemyShip.CollisionDamage);
                prefab = PoolManager.GetObject
                    (_explosionShip, _enemyShip.transform.position, Quaternion.identity);
                StartCoroutine(ReturnToPool(prefab));
                _enemyShip.GetComponent<PoolObject>().ReturnToPool();
                EnemyManager.RemoveEnemieToList(_enemyShip);
                AudioManager.Instance.PlaySound(_explosionShipSound);
            }
            else if (_asteroid)
            {
                DamageTaken(_asteroid.CollisionDamage);
                prefab = PoolManager.GetObject
                    (_explosionAsteroid, this.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(ReturnToPool(prefab));
                _asteroid.GetComponent<PoolObject>().ReturnToPool();
                AudioManager.Instance.PlaySound(_explosionAsteroidSound);
            }

            if (_currentHealth <= _minHealth)
            {
                prefab = PoolManager.GetObject
                    (_explosionShip, this.gameObject.transform.position, Quaternion.identity);
                //explosion add to pool object
                StartCoroutine(ReturnToPool(prefab));
                this.gameObject.GetComponent<PoolObject>().ReturnToPool();
                AudioManager.Instance.PlaySound(_explosionShipSound);
            }
        }
    }

    /// <summary>
    /// Обработка полученного урона с учетом SpaceshipShield
    /// </summary>
    /// <param name="damage">Полученный урон</param>
    private void DamageTaken(int damage)
    {
        if (_shield.CurrentShield >= damage)
        {
            _shield.CurrentShield -= damage;
        }
        else
        {
            _tempValue = damage - _shield.CurrentShield;
            _shield.CurrentShield = _shieldDestroyed;
            CurrentHealth -= _tempValue;
        }
    }

    /// <summary>
    /// Пополнение здоровья.
    /// </summary>
    public void HealthTaken(float health)
    {
        CurrentHealth += health;
    }
}