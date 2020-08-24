using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipEnemy : BaseObjectScene, IFire, IMove, IExecute
{
    [SerializeField] private readonly float _speed = 10.0f;
    [SerializeField] private Transform _gun;
    [SerializeField] private float _collisionDamage = 50.0f;

    private readonly string _bulletYellow = "BulletYellow";
    private readonly string _explosionShip = "ShipExplosion";
    private readonly float _forceAmmunition = -75.0f;

    private float _delay = 2.0f;
    private float _nextLaunchTime = 2.0f;

    private float _rayDistance = 100.0f;
    private Ray _ray;
    private RaycastHit _hit;

    private Bullet _bullet;
    private SpaceshipModel _spaceship;

    public System.Action<SpaceshipEnemy> OnDieChange;

    protected override void Awake()
    {
        base.Awake();
        Move();
    }

    public float CollisionDamage
    {
        get { return _collisionDamage; }
        set { _collisionDamage = value; }
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
            OnDieChange?.Invoke(this);

            ScoreUI.instance.Score += 10;
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
        if (Physics.Raycast(_ray, out _hit, _rayDistance))
        {
            _spaceship = _hit.collider.gameObject.GetComponent<SpaceshipModel>();

            if (_spaceship && Time.time > _nextLaunchTime)
            {
                prefab = PoolManager.GetObject(_bulletYellow, _gun.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                _nextLaunchTime = Time.time + _delay;
            }
        }
    }

    public void Move()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.velocity = -new Vector3(0, 0, _speed);
    }
}