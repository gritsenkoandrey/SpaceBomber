using UnityEngine;


public sealed class SpaceshipEnemy : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    private float _forceAmmunition = -2500.0f;
    private float _delay = 2.0f;
    private float _nextLaunchTime = 2.0f;

    private Ray _ray;
    private RaycastHit _hit;
    private float _rayDistance = 100.0f;

    [SerializeField] private GameObject _explosion;
    [SerializeField] private Transform _gun;
    [SerializeField] private Bullet _ammunition;
    private Bullet _bullet;
    private Rigidbody _ship;
    private AsteroidModel _asteroid;
    private SpaceshipModel _spaceship;

    private void Start()
    {
        _ship = GetComponent<Rigidbody>();
        _ship.velocity = -new Vector3(0, 0, _speed);
    }

    private void Update()
    {
        Fire();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _asteroid = other.gameObject.GetComponent<AsteroidModel>();
        _spaceship = other.gameObject.GetComponent<SpaceshipModel>();

        if (_bullet /*|| _asteroid */|| _spaceship)
        {
            Instantiate(_explosion, this.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }

    private void Fire()
    {
        _ray = new Ray(_gun.position, _gun.forward * -_rayDistance);

        if (Physics.Raycast(_ray, out _hit, _rayDistance))
        {
            _spaceship = _hit.collider.gameObject.GetComponent<SpaceshipModel>();

            if (_spaceship && Time.time > _nextLaunchTime)
            {
                _bullet = Instantiate(_ammunition, _gun.position, Quaternion.identity);
                _bullet.AddForce(_gun.forward * _forceAmmunition);
                _nextLaunchTime = Time.time + _delay;
            }
        }
    }
}