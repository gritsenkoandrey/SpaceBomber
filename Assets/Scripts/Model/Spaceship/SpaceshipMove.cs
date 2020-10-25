using Assets.Scripts.Interface;
using UnityEngine;


public sealed class SpaceshipMove : SpaceshipModel, IMove
{
    [SerializeField] private float _normalSpeed = 0.0f;
    private float _speed;

    private readonly float _xMin = -40.0f;
    private readonly float _xMax = 40.0f;
    private readonly float _zMin = -50.0f;
    private readonly float _zMax = 60.0f;

    private float _clampPosX;
    private float _clampPosZ;

    private readonly float _tilt = 25.0f;

    private Vector2 _input;

    public float CurrentSpeed
    {
        get { return _speed; }
        private set { _speed = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _speed = _normalSpeed;
    }

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

    /// <summary>
    /// Изменение скорости передвижения корабля.
    /// </summary>
    /// <param name="speed">Показатель на которую прибавляется скорость</param>
    /// <param name="time">Время на которое прибавляется скорость</param>
    public void PickEnergyItem(float speed, float time)
    {
        CurrentSpeed += speed;
        Invoke(nameof(ReturnToNormalSpeed), time);
    }

    private void ReturnToNormalSpeed()
    {
        CurrentSpeed = _normalSpeed;
    }
}