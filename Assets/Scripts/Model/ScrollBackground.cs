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

    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
        _currentSpeed = _speed;
    }

    public void Move()
    {
        if (ScoreUI.instance.Score < 50)
        {
            _move = Mathf.Repeat(Time.time * _currentSpeed, _backgroundSize);
            transform.position = _startPos + new Vector3(0, 0, -_move);
        }

        if (ScoreUI.instance.Score >= 50)
        {
            _move = Mathf.Repeat(Time.time * _currentSpeed * 2, _backgroundSize);
            transform.position = _startPos + new Vector3(0, 0, -_move);
        }

        if (ScoreUI.instance.Score >= 100)
        {
            _move = Mathf.Repeat(Time.time * _currentSpeed * 4, _backgroundSize);
            transform.position = _startPos + new Vector3(0, 0, -_move);
        }

        if (ScoreUI.instance.Score >= 150)
        {
            _move = Mathf.Repeat(Time.time * _currentSpeed * 6, _backgroundSize);
            transform.position = _startPos + new Vector3(0, 0, -_move);
        }
    }
}