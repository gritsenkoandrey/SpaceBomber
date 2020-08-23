using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private float _maxHealth = 100.0f;
    private float _currentHealth;
    private readonly float _minHealth = 0;
    private readonly float _quarter = 0.25f;
    private readonly float _maxPercent = 100.0f;
    private readonly string _explosionShip = "ShipExplosion";

    private Bullet _bullet;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public float FillHealth
    {
        get { return _currentHealth / _maxHealth; }
    }

    public float AverageHealt
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
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();

        if (_bullet)
        {
            CurrentHealth -= _bullet.Damage;
            _bullet.GetComponent<PoolObject>().ReturnToPool();
            if (_currentHealth <= 0)
            {
                prefab = PoolManager.GetObject(_explosionShip,
                    this.gameObject.transform.position, Quaternion.identity);
                //explosion add to pool object
                StartCoroutine(ReturnToPool(prefab));
                
                this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            }
        }
        else
        {
            return;
        }
    }
}