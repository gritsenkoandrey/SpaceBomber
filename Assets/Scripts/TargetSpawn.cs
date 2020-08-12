using UnityEngine;


public sealed class TargetSpawn : MonoBehaviour
{
    [SerializeField] private AsteroidModel[] _asteroids;

    private float _minDelay = 0.25f;
    private float _maxdelay = 2.0f;
    private float _nextLaunchTime = 0.25f;

    private float _posX;
    private float _posY = 0;
    private float _posZ;

    private void Start()
    {
        _posZ = transform.position.z;
    }

    private void Update()
    {
        if (Time.time > _nextLaunchTime)
        {
            _posX = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            Instantiate(_asteroids[Random.Range(0, _asteroids.Length)],
                new Vector3(_posX, _posY, _posZ), Quaternion.identity);
            _nextLaunchTime = Time.time + Random.Range(_minDelay, _maxdelay);
        }
    }
}