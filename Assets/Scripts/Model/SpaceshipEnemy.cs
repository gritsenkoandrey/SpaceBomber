using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Manager;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using Assets.Scripts.TimeRemainings;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public sealed class SpaceshipEnemy : BaseObjectScene, IFire, IMove, IExecute
{
    private readonly float _minSpeed = 20.0f;
    private readonly float _maxSpeed = 50.0f;
    [SerializeField] private int _collisionDamage = 50;

    private readonly string _bulletYellowPrefab = "BulletYellow";
    private readonly string _explosionShipPrefab = "ShipExplosion";
    private readonly string _explosionShipSound = "explosion_spaceship";
    private readonly string _audioBullet = "laser_spaceship_03";

    private readonly float _forceAmmunition = -75.0f;

    private float _nextLaunchTime = 2.0f;
    private bool _isReady = true;
    private TimeRemaining _timeRemaining;

    private readonly byte _points = 10;

    private readonly float _rayDistance = 100.0f;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Transform _gun;
    private Bullet _bullet;
    private SpaceshipModel _spaceship;
    private Spawn _spawn;

    public event Action<SpaceshipEnemy> OnDieChange;

    protected override void Awake()
    {
        base.Awake();
        Move();
        _spawn = FindObjectOfType<Spawn>();
        _timeRemaining = new TimeRemaining(ReadyShoot, _nextLaunchTime);
    }

    public int CollisionDamage
    {
        get { return _collisionDamage; }
        private set { _collisionDamage = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();

        if (_bullet)
        {
            _bullet.GetComponent<PoolObject>().ReturnToPool();
            prefab = PoolManager.GetObject
                (_explosionShipPrefab, this.gameObject.transform.position, Quaternion.identity);
            //explosion return to pool
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            AudioManager.Instance.PlaySound(_explosionShipSound);
            OnDieChange?.Invoke(this);
            _spawn.SpawnPickItems(this.gameObject.transform.position);
            ScoreUI.instance.Score += _points;
        }
        else
        {
            return;
        }
    }

    public void Execute()
    {
        Fire();
    }

    public void Fire()
    {
        _ray = new Ray(_gun.position, -_gun.forward);
        if (Physics.Raycast(_ray, out _hit, _rayDistance, _layerMask))
        {
            _spaceship = _hit.collider.gameObject.GetComponent<SpaceshipModel>();

            if (_spaceship && _isReady)
            {
                prefab = PoolManager.GetObject(_bulletYellowPrefab, _gun.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                AudioManager.Instance.PlaySound(_audioBullet);
                _isReady = false;
                _timeRemaining.AddTimeRemaining();
            }
        }
    }

    public void Move()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = -new Vector3(0, 0, Random.Range(_minSpeed, _maxSpeed));
    }

    private void ReadyShoot()
    {
        _isReady = true;
    }
}