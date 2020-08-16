using Assets.Scripts;
using Assets.Scripts.PoolObject;
using System.Collections;
using UnityEngine;


public sealed class AsteroidModel : MonoBehaviour
{
    private float _rotation;
    private float _minRotation = 5.0f;
    private float _maxRotation = 20.0f;

    private float _minSpeed = 5.0f;
    private float _maxSpeed = 25.0f;

    private readonly string _explosionAsteroid = "AsteroidExplosion";
    private readonly string _explosionShip = "ShipExplosion";
    private readonly float _returnToPool = 1.5f;

    private Rigidbody _asteroid;
    private Bullet _bullet;
    private SpaceshipModel _ship;
    private SpaceshipEnemy _shipEnemy;
    private GameObject _prefab;

    //public System.Action OnPointChange = delegate { };

    private void Start()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _ship = other.gameObject.GetComponent<SpaceshipModel>();
        _shipEnemy = other.gameObject.GetComponent<SpaceshipEnemy>();

        if (_bullet)
        {
            _prefab = PoolManager.GetObject(_explosionAsteroid, 
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(_prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            _bullet.GetComponent<PoolObject>().ReturnToPool();

            ScoreController.instance.Score += 10;
            //OnPointChange.Invoke();
        }
        else if (_ship)
        {
            _prefab = PoolManager.GetObject(_explosionShip,
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(_prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            Destroy(_ship.gameObject);
        }
        else if (_shipEnemy)
        {
            _prefab = PoolManager.GetObject(_explosionAsteroid,
                this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(_prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
        }
        else
        {
            return;
        }
    }

    private void Move()
    {
        _asteroid = GetComponent<Rigidbody>();
        _rotation = Random.Range(_minRotation, _maxRotation);
        // случайное вращение астеройда
        _asteroid.angularVelocity = Random.insideUnitSphere * _rotation;
        _asteroid.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
    }

    private IEnumerator ReturnToPool(GameObject obj)
    {
        yield return new WaitForSeconds(_returnToPool);
        obj.GetComponent<PoolObject>().ReturnToPool();
    }
}