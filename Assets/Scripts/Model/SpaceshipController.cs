using Assets.Scripts.Interface;
using UnityEngine;


public sealed class SpaceshipController : SpaceshipModel, IMove
{
    [SerializeField] private float _speed = 30.0f;

    private float _xMin = -35.0f;
    private float _xMax = 35.0f;
    private float _zMin = -55.0f;
    private float _zMax = 35.0f;

    private float _clampPosX;
    private float _clampPosZ;

    private float _tilt = 25.0f;

    private float _moveHorizontal;
    private float _moveVertical;

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveVertical = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector3(_moveHorizontal, 0, _moveVertical) * _speed;

        // границы карты
        _clampPosX = Mathf.Clamp(rigidbody.position.x, _xMin, _xMax);
        _clampPosZ = Mathf.Clamp(rigidbody.position.z, _zMin, _zMax);
        rigidbody.position = new Vector3(_clampPosX, 0, _clampPosZ);

        // поворот корабля
        rigidbody.rotation = Quaternion.Euler(_moveVertical * _tilt, 0, -_moveHorizontal * _tilt);
    }
}