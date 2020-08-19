using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class AsteroidModel : BaseObjectScene, IMove
{
    private float _rotation;
    private float _minRotation = 5.0f;
    private float _maxRotation = 20.0f;

    private float _minSpeed = 5.0f;
    private float _maxSpeed = 25.0f;

    private readonly string _explosionAsteroid = "AsteroidExplosion";
    private readonly string _explosionShip = "ShipExplosion";

    private Bullet _bullet;
    private SpaceshipModel _ship;
    private SpaceshipEnemy _shipEnemy;

    //public System.Action OnPointChange = delegate { };
    protected override void Awake()
    {
        base.Awake();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _ship = other.gameObject.GetComponent<SpaceshipModel>();
        _shipEnemy = other.gameObject.GetComponent<SpaceshipEnemy>();

        if (_bullet)
        {
            prefab = PoolManager.GetObject(_explosionAsteroid, 
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            _bullet.GetComponent<PoolObject>().ReturnToPool();

            ScoreUI.instance.Score += 10;
            //OnPointChange.Invoke();
        }
        else if (_ship)
        {
            prefab = PoolManager.GetObject(_explosionShip,
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            
            _ship.GetComponent<PoolObject>().ReturnToPool();
        }
        else if (_shipEnemy)
        {
            prefab = PoolManager.GetObject(_explosionAsteroid,
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
        }
        else
        {
            return;
        }
    }

    public void Move()
    {
        _rotation = Random.Range(_minRotation, _maxRotation);
        // случайное вращение астеройда
        rigidbody.angularVelocity = Random.insideUnitSphere * _rotation;
        rigidbody.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
    }
}