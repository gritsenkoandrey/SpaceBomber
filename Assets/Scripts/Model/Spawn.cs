using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using Assets.Scripts.TimeRemainings;
using UnityEngine;


public sealed class Spawn : BaseObjectScene
{
    private readonly string[] _asteroids = { "Asteroid_1", "Asteroid_2",
        "Asteroid_3", "Asteroid_4", "Asteroid_5" };

    private readonly string[] _enemyShips = { "EnemySpaceship_1", "EnemySpaceship_2", 
        "EnemySpaceship_3", "EnemySpaceship_4", "EnemySpaceship_5", "EnemySpaceship_6",
        "EnemySpaceship_7", "EnemySpaceship_8", "EnemySpaceship_9", "EnemySpaceship_10",
        "EnemySpaceship_11", "EnemySpaceship_12", "EnemySpaceship_13" };

    /// <summary>
    /// Массив предметов, которые спавнятся после убийства вражеского врага.
    /// </summary>
    private readonly string[] _pickItems = { "HealthItem", "ShieldItem", "RapidFireItem" };

    private readonly float _minDelay = 2.0f;
    private readonly float _maxDelay = 7.0f;

    private float _nextAsteroid = 2.0f;
    private float _nextSpaceshipEnemy = 2.5f;

    private float _posX;
    private readonly float _posY = 0;
    private float _posZ;
    private readonly float _half = 0.5f;

    private float _difficulty;
    private readonly float _addDifficulty = 0.15f;

    private GameObject _obj;
    private TimeRemaining _timeRemainingAsteroid;
    private TimeRemaining _timeRemainingSpaceship;
    private bool _isReadyAsteroid = true;
    private bool _isReadySpaceship = true;

    protected override void Awake()
    {
        base.Awake();
        _posZ = transform.position.z;
        _timeRemainingAsteroid = new TimeRemaining(ReadyToSpawnAsteroid, _nextAsteroid);
        _timeRemainingSpaceship = new TimeRemaining(ReadyToSpawnSpaceship, _nextSpaceshipEnemy);
    }

    public void SpawnAsteroid()
    {
        if (_isReadyAsteroid)
        {
            _posX = RandomPosX();
            _difficulty += _addDifficulty;

            _obj = PoolManager.GetObject(_asteroids[Random.Range(0, _asteroids.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);

            _isReadyAsteroid = false;
            _timeRemainingAsteroid.AddTimeRemaining();
            _timeRemainingAsteroid.CurrentTime /= _difficulty;
        }
    }

    public void SpawnSpaceshipEnemies()
    {
        if (_isReadySpaceship)
        {
            _posX = RandomPosX();

            _obj = PoolManager.GetObject(_enemyShips[Random.Range(0, _enemyShips.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            EnemyManager.AddEnemieToList(_obj.GetComponent<SpaceshipEnemy>());

            _isReadySpaceship = false;
            _timeRemainingSpaceship.AddTimeRemaining();
            _timeRemainingSpaceship.CurrentTime = Random.Range(_minDelay, _maxDelay);
        }
    }

    public void SpawnPickItems(Vector3 pos)
    {
        PoolManager.GetObject(_pickItems[Random.Range(0, _pickItems.Length)], pos, Quaternion.identity);
    }

    private float RandomPosX()
    {
        return Random.Range(-transform.localScale.x * _half, transform.localScale.x * _half);
    }

    private void ReadyToSpawnAsteroid()
    {
        _isReadyAsteroid = true;
    }

    private void ReadyToSpawnSpaceship()
    {
        _isReadySpaceship = true;
    }
}