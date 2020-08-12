using UnityEngine;


public sealed class SpaceshipController : SpaceshipModel
{
    [SerializeField] private float _speed = 30.0f;

    private float _xMin = -25.0f;
    private float _xMax = 25.0f;
    private float _zMin = -45.0f;
    private float _zMax = 25.0f;
    private float _clampPosX;
    private float _clampPosZ;

    private float _tilt = 25.0f;
    private float _moveHorizontal;
    private float _moveVertical;

    private void Update()
    {
        MoveShip();
    }

    private void MoveShip()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveVertical = Input.GetAxis("Vertical");
        _ship.velocity = new Vector3(_moveHorizontal, 0, _moveVertical) * _speed;

        // границы карты
        _clampPosX = Mathf.Clamp(_ship.position.x, _xMin, _xMax);
        _clampPosZ = Mathf.Clamp(_ship.position.z, _zMin, _zMax);
        _ship.position = new Vector3(_clampPosX, 0, _clampPosZ);

        // поворот корабля
        _ship.rotation = Quaternion.Euler(_moveVertical * _tilt, 0, -_moveHorizontal * _tilt);
    }
}