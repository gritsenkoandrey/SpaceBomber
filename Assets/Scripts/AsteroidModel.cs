using UnityEngine;


public sealed class AsteroidModel : MonoBehaviour
{
    private float _rotation;
    private float _minRotation = 5.0f;
    private float _maxRotation = 20.0f;

    private float _minSpeed = 5.0f;
    private float _maxSpeed = 25.0f;

    [SerializeField] GameObject _explosionAsteroid;
    [SerializeField] GameObject _explosionShip;

    private Rigidbody _asteroid;
    private Bullet _bullet;
    private SpaceshipModel _ship;

    private void Start()
    {
        _asteroid = GetComponent<Rigidbody>();
        _rotation = Random.Range(_minRotation, _maxRotation);

        // случайное вращение астеройда
        _asteroid.angularVelocity = Random.insideUnitSphere * _rotation;

        _asteroid.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _ship = other.gameObject.GetComponent<SpaceshipModel>();

        if (_bullet)
        {
            Instantiate(_explosionAsteroid, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(_bullet.gameObject);
        }
        else if (_ship)
        {
            Instantiate(_explosionShip, _ship.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Destroy(_ship.gameObject);
        }
        else
        {
            return;
        }
    }
}