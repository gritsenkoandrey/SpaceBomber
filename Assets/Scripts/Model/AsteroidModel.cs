using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class AsteroidModel : BaseObjectScene, IMove
{
    [SerializeField] private float _collisionDamage = 20.0f;

    private float _rotation;
    private readonly float _minRotation = 5.0f;
    private readonly float _maxRotation = 20.0f;

    private readonly float _minSpeed = 5.0f;
    private readonly float _maxSpeed = 25.0f;

    private readonly string _explosionAsteroid = "AsteroidExplosion";

    private Bullet _bullet;

    public float CollisionDamage
    {
        get { return _collisionDamage; }
        set { _collisionDamage = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        if (_bullet)
        {
            prefab = PoolManager.GetObject
                (_explosionAsteroid, this.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(ReturnToPool(prefab));
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            _bullet.GetComponent<PoolObject>().ReturnToPool();

            ScoreUI.instance.Score += 10;
        }
    }

    public void Move()
    {
        _rotation = Random.Range(_minRotation, _maxRotation);
        // случайное вращение астеройда
        Rigidbody.angularVelocity = Random.insideUnitSphere * _rotation;
        Rigidbody.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
    }
}