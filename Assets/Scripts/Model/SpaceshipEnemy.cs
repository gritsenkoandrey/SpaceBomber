using Assets.Scripts;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipEnemy : SpaceshipEnemyModel, IFire
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private Transform _gun;

    private readonly string _bulletYellow = "BulletYellow";
    private readonly string _explosionShip = "ShipExplosion";
    private readonly float _forceAmmunition = 2500.0f;

    private float _delay = 2.0f;
    private float _nextLaunchTime = 2.0f;

    private float _rayDistance = 100.0f;
    private Ray _ray;
    private RaycastHit _hit;

    //public System.Action OnPointChange = delegate { };

    private void Start()
    {
        ship = GetComponent<Rigidbody>();
        ship.velocity = -new Vector3(0, 0, _speed);
    }

    private void Update()
    {
        Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        bullet = other.gameObject.GetComponent<Bullet>();
        spaceship = other.gameObject.GetComponent<SpaceshipModel>();

        if (bullet || spaceship)
        {
            PoolManager.GetObject(_explosionShip, this.gameObject.transform.position, Quaternion.identity);
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            //OnPointChange.Invoke();
            ScoreController.instance.Score += 10;

            if (bullet)
            {
                bullet.GetComponent<PoolObject>().ReturnToPool();
            }
            else
            {
                Destroy(spaceship.gameObject);
            }
        }
        else
        {
            return;
        }
    }

    //todo почему со временем пули у врагов начинают лететь быстрее
    public void Fire()
    {
        _ray = new Ray(_gun.position, -_gun.forward);

        if (Physics.Raycast(_ray, out _hit, _rayDistance))
        {
            spaceship = _hit.collider.gameObject.GetComponent<SpaceshipModel>();

            if (spaceship && Time.time > _nextLaunchTime)
            {
                prefab = PoolManager.GetObject(_bulletYellow, _gun.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().AddForce(-_gun.forward * _forceAmmunition);
                _nextLaunchTime = Time.time + _delay;
            }
        }
    }
}