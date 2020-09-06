using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class Spawn : BaseObjectScene
{
    private readonly string[] _asteroids = { "Asteroid_1", "Asteroid_2",
        "Asteroid_3", "Asteroid_4", "Asteroid_5" };
    private readonly string[] _enemyShips = { "EnemySpaceship_1", "EnemySpaceship_2", 
        "EnemySpaceship_3", "EnemySpaceship_4", "EnemySpaceship_5,", "EnemySpaceship_6",
        "EnemySpaceship_7", "EnemySpaceship_8", "EnemySpaceship_9", "EnemySpaceship_10",
        "EnemySpaceship_11", "EnemySpaceship_12", "EnemySpaceship_13" };

    private readonly string[] _pickItems = { "EnergyItem", "HealthItem", "ShieldItem" };

    private readonly float _minDelay = 1.0f;
    private readonly float _maxDelay = 5.0f;

    private float _nextAsteroid = 2.0f;
    private float _nextSpaceshipEnemy = 1.0f;
    private float _nextPickItem = 5.0f;

    private float _posX;
    private readonly float _posY = 0;
    private float _posZ;
    private readonly float _half = 0.5f;

    private float _difficulty;
    private readonly float _addDifficulty = 0.15f;

    private GameObject _enemie;

    protected override void Awake()
    {
        base.Awake();
        _posZ = transform.position.z;
    }

    public void SpawnAsteroid()
    {
        if (Time.time > _nextAsteroid)
        {
            _posX = RandomPosX();
            _difficulty += _addDifficulty;

            PoolManager.GetObject(_asteroids[Random.Range(0, _asteroids.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextAsteroid = Time.time + Random.Range(_minDelay, _maxDelay) / _difficulty;
        }
    }

    public void SpawnSpaceshipEnemies()
    {
        if (Time.time > _nextSpaceshipEnemy)
        {
            _posX = RandomPosX();

            _enemie = PoolManager.GetObject(_enemyShips[Random.Range(0, _enemyShips.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            //todo разобраться почему иногда прилетает null
            if (_enemie != null)
            {
                EnemyManager.AddEnemieToList(_enemie.GetComponent<SpaceshipEnemy>());
            }
            _nextSpaceshipEnemy = Time.time + Random.Range(_minDelay, _maxDelay);
        }
    }

    public void SpawnPickItems()
    {
        if (Time.time > _nextPickItem)
        {
            _posX = RandomPosX();

            PoolManager.GetObject(_pickItems[Random.Range(0, _pickItems.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextPickItem = Time.time + _maxDelay;
        }
    }

    private float RandomPosX()
    {
        return Random.Range(-transform.localScale.x * _half, transform.localScale.x * _half);
    }
}