using Assets.Scripts.Interface;
using UnityEngine;


public sealed class SpaceshipMove : SpaceshipModel, IMove
{
    [SerializeField] private float _speed = 30.0f;

    private float _xMin = -40.0f;
    private float _xMax = 40.0f;
    private float _zMin = -50.0f;
    private float _zMax = 60.0f;

    private float _clampPosX;
    private float _clampPosZ;

    private float _tilt = 25.0f;

    private Vector2 _input;

    public void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Rigidbody.velocity = new Vector3(_input.x, 0, _input.y) * _speed;

        // граница карты
        _clampPosX = Mathf.Clamp(Rigidbody.position.x, _xMin, _xMax);
        _clampPosZ = Mathf.Clamp(Rigidbody.position.z, _zMin, _zMax);
        Rigidbody.position = new Vector3(_clampPosX, 0, _clampPosZ);

        // поворот корабля
        Rigidbody.rotation = Quaternion.Euler(_input.y, 0, -_input.x * _tilt);
    }
}