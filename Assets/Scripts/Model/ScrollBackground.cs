using Assets.Scripts.Model;
using UnityEngine;


public sealed class ScrollBackground : BaseObjectScene
{
    [SerializeField] private float _speed;
    private float _move;
    private Vector3 _startPos;

    protected override void Awake()
    {
        base.Awake();
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        _move = Mathf.Repeat(Time.time * _speed, 150);
        transform.position = _startPos + new Vector3(0, 0, -_move);
    }
}