using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using UnityEngine;


public sealed class ScrollBackground : BaseObjectScene, IMove
{
    [SerializeField] private float _speed = 30.0f;
    private float _currentSpeed;

    private float _move;
    private readonly float _backgroundSize = 300.0f;
    private Vector3 _startPos;

    public float CurrentSpeed
    {
        get { return _currentSpeed; }
        private set { _currentSpeed = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
        _currentSpeed = _speed;
    }

    public void Move()
    {
        CurrentMove(0, _currentSpeed);
        CurrentMove(50, _currentSpeed * 2);
        CurrentMove(200, _currentSpeed * 3);
        CurrentMove(400, _currentSpeed * 4);
        CurrentMove(800, _currentSpeed * 5);
        CurrentMove(1200, _currentSpeed * 6);
        CurrentMove(1500, _currentSpeed * 7);
        CurrentMove(2000, _currentSpeed * 8);
        CurrentMove(2500, _currentSpeed * 9);
        CurrentMove(3500, _currentSpeed * 10);
    }

    private void CurrentMove(int countScore, float speed)
    {
        if (ScoreUI.instance.Score >= countScore)
        {
            _move = Mathf.Repeat(Time.time * speed, _backgroundSize);
            transform.position = _startPos + new Vector3(0, 0, -_move);
        }
    }

    // доработать метод
    public void PickEnergyItem(float speed, float time)
    {
        CurrentSpeed += speed;
        Invoke(nameof(ReturnToNormalSpeed), time);
    }

    private void ReturnToNormalSpeed()
    {
        CurrentSpeed = _speed;
    }
}