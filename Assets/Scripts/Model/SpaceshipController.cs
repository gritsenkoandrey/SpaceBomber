using UnityEngine;


public sealed class SpaceshipController : SpaceshipModel
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
        MoveShip();
    }

    private void MoveShip()
    {
        _moveHorizontal = Input.GetAxis("Horizontal");
        _moveVertical = Input.GetAxis("Vertical");
        ship.velocity = new Vector3(_moveHorizontal, 0, _moveVertical) * _speed;

        // границы карты
        _clampPosX = Mathf.Clamp(ship.position.x, _xMin, _xMax);
        _clampPosZ = Mathf.Clamp(ship.position.z, _zMin, _zMax);
        ship.position = new Vector3(_clampPosX, 0, _clampPosZ);

        // поворот корабля
        ship.rotation = Quaternion.Euler(_moveVertical * _tilt, 0, -_moveHorizontal * _tilt);
    }
}