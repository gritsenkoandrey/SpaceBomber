using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipEnemy : SpaceshipEnemyModel, IFire, IMove
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private Transform _gun;

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

    //public System.Action OnPointChange = delegate { };

    private void Start()
    {
        Move();
    }

    private void Update()
    {
        Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _spaceship = other.gameObject.GetComponent<SpaceshipModel>();

        if (_bullet || _spaceship)
        {
            prefab = PoolManager.GetObject(_explosionShip,
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            //OnPointChange.Invoke();
            ScoreUI.instance.Score += 10;

            if (_bullet)
            {
                _bullet.GetComponent<PoolObject>().ReturnToPool();
            }
            else
            {
                Destroy(_spaceship.gameObject);
            }
        }
        else
        {
            return;
        }
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
                //prefab.GetComponent<Bullet>().AddForce(-_gun.forward * _forceAmmunition);
                _nextLaunchTime = Time.time + _delay;
            }
        }
    }

    public void Move()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = -new Vector3(0, 0, _speed);
    }
}