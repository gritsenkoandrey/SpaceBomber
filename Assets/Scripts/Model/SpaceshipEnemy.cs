using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Manager;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


public sealed class SpaceshipEnemy : BaseObjectScene, IFire, IMove, IExecute
{
    private readonly float _minSpeed = 20.0f;
    private readonly float _maxSpeed = 50.0f;
    [SerializeField] private int _collisionDamage = 50;

    private readonly string _bulletYellow = "BulletYellow";
    private readonly string _explosionShip = "ShipExplosion";
    private readonly string _explosionShipSound = "Grenade6Short";
    private readonly string _bulletSound = "Laser19";

    private readonly float _forceAmmunition = -75.0f;

    private readonly float _delay = 2.0f;
    private float _nextLaunchTime = 2.0f;

    private readonly byte _points = 10;

    private readonly float _rayDistance = 100.0f;
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private Transform _gun;
    private Bullet _bullet;
    private SpaceshipModel _spaceship;

    public event Action<SpaceshipEnemy> OnDieChange;

    protected override void Awake()
    {
        base.Awake();
        Move();
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
                (_explosionShip, this.gameObject.transform.position, Quaternion.identity);
            //explosion return to pool
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            AudioManager.Instance.PlaySound(_explosionShipSound);
            OnDieChange?.Invoke(this);

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

            if (_spaceship && Time.time > _nextLaunchTime)
            {
                prefab = PoolManager.GetObject(_bulletYellow, _gun.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                AudioManager.Instance.PlaySound(_bulletSound);
                _nextLaunchTime = Time.time + _delay;
            }
        }
    }

    public void Move()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = -new Vector3(0, 0, Random.Range(_minSpeed, _maxSpeed));
    }
}