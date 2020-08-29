using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using UnityEngine;


public sealed class ScrollBackground : BaseObjectScene, IMove
{
    [SerializeField] private float _speed = 30.0f;
    private float _move;
    private readonly float _backgroundSize = 150.0f;
    private Vector3 _startPos;

    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
    }

    public void Move()
    {
        _move = Mathf.Repeat(Time.time * _speed, _backgroundSize);
        transform.position = _startPos + new Vector3(0, 0, -_move);
    }
}