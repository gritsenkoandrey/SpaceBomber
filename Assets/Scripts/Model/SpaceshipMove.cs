using Assets.Scripts.Interface;
using UnityEngine;


public sealed class SpaceshipMove : SpaceshipModel, IMove
{
    [SerializeField] private float _speed = 30.0f;

    private float _xMin = -35.0f;
    private float _xMax = 35.0f;
    private float _zMin = -60.0f;
    private float _zMax = 35.0f;

    private float _clampPosX;
    private float _clampPosZ;

    private float _tilt = 25.0f;

    private Vector2 _input;

    public void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody.velocity = new Vector3(_input.x, 0, _input.y) * _speed;

        // граница карты
        _clampPosX = Mathf.Clamp(rigidbody.position.x, _xMin, _xMax);
        _clampPosZ = Mathf.Clamp(rigidbody.position.z, _zMin, _zMax);
        rigidbody.position = new Vector3(_clampPosX, 0, _clampPosZ);

        // поворот корабля
        rigidbody.rotation = Quaternion.Euler(_input.y * _tilt, 0, _input.x * _tilt);
    }
}