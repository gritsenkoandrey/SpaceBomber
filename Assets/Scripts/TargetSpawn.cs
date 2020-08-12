using UnityEngine;


public sealed class TargetSpawn : MonoBehaviour
{
    [SerializeField] private AsteroidModel[] _asteroids;
    [SerializeField] private SpaceshipEnemy _spaceshipEnemy;

    private float _minDelay = 1.0f;
    private float _maxdelay = 4.0f;

    private float _nextAsteroid = 2.0f;
    private float _nextSpaceshipEnemy = 2.0f;

    private float _posX;
    private float _posY = 0;
    private float _posZ;

    private void Start()
    {
        _posZ = transform.position.z;
    }

    private void Update()
    {
        _posX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

        SpawnAsteroid();
        SpawnSpaceshipEnemy();
    }

    private void SpawnAsteroid()
    {
        if (Time.time > _nextAsteroid)
        {
            //_posX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

            Instantiate(_asteroids[Random.Range(0, _asteroids.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextAsteroid = Time.time + Random.Range(_minDelay, _maxdelay);
        }
    }

    private void SpawnSpaceshipEnemy()
    {
        if (Time.time > _nextSpaceshipEnemy)
        {
            //_posX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

            Instantiate(_spaceshipEnemy, new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextSpaceshipEnemy = Time.time + Random.Range(_minDelay, _maxdelay);
        }
    }
}