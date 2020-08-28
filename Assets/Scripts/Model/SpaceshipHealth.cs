using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private float _maxHealth = 100.0f;
    private float _currentHealth;
    private readonly float _minHealth = 0;
    private readonly float _quarter = 0.25f;
    private readonly float _maxPercent = 100.0f;

    private float _tempValue;
    private readonly float _minShield = 0;

    private readonly string _explosionShip = "ShipExplosion";
    private readonly string _explosionAsteroid = "AsteroidExplosion";

    private Bullet _bullet;
    private SpaceshipEnemy _enemyShip;
    private AsteroidModel _asteroid;
    private SpaceshipShield _shield;

    public float CurrentHealth
    {
        get { return _currentHealth; }
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
        _shield = GetComponent<SpaceshipShield>();
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
            }
            else if (_asteroid)
            {
                DamageTaken(_asteroid.CollisionDamage);
                prefab = PoolManager.GetObject
                    (_explosionAsteroid, this.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(ReturnToPool(prefab));
                _asteroid.GetComponent<PoolObject>().ReturnToPool();
            }

            if (_currentHealth <= _minHealth)
            {
                prefab = PoolManager.GetObject
                    (_explosionShip, this.gameObject.transform.position, Quaternion.identity);
                //explosion add to pool object
                StartCoroutine(ReturnToPool(prefab));
                this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            }
        }
    }

    private void DamageTaken(float damage)
    {
        if (_shield.CurrentShield >= damage)
        {
            _shield.CurrentShield -= damage;
        }
        else
        {
            _tempValue = damage - _shield.CurrentShield;
            _shield.CurrentShield = _minShield;
            CurrentHealth -= _tempValue;
        }
    }
}