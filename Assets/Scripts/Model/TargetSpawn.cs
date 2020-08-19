using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class TargetSpawn : BaseObjectScene
{
    private readonly string[] _asteroids = { "Asteroid_1", "Asteroid_2",
        "Asteroid_3", "Asteroid_4", "Asteroid_5" };
    private readonly string[] _enemyShips = { "EnemySpaceship_1", "EnemySpaceship_2", 
        "EnemySpaceship_3", "EnemySpaceship_4", "EnemySpaceship_5,", "EnemySpaceship_6",
        "EnemySpaceship_7", "EnemySpaceship_8", "EnemySpaceship_9", "EnemySpaceship_10",
        "EnemySpaceship_11", "EnemySpaceship_12", "EnemySpaceship_13" };

    private float _minDelay = 1.0f;
    private float _maxdelay = 4.0f;

    private float _nextAsteroid = 2.0f;
    private float _nextSpaceshipEnemy = 2.0f;

    private float _posX;
    private float _posY = 0;
    private float _posZ;

    private float _difficulty;

    protected override void Awake()
    {
        base.Awake();
        _posZ = transform.position.z;
    }

    private void FixedUpdate()
    {
        _posX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);

        SpawnAsteroid();
        SpawnSpaceshipEnemy();
    }

    private void SpawnAsteroid()
    {
        if (Time.time > _nextAsteroid)
        {
            _difficulty += 0.15f;

            PoolManager.GetObject(_asteroids[Random.Range(0, _asteroids.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextAsteroid = Time.time + Random.Range(_minDelay, _maxdelay) / _difficulty;
        }
    }

    private void SpawnSpaceshipEnemy()
    {
        if (Time.time > _nextSpaceshipEnemy)
        {
            PoolManager.GetObject(_enemyShips[Random.Range(0, _enemyShips.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextSpaceshipEnemy = Time.time + Random.Range(_minDelay, _maxdelay);
        }
    }
}