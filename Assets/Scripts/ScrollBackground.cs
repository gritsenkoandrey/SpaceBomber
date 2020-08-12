using UnityEngine;


public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _move;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        _move = Mathf.Repeat(Time.time * _speed, 150);
        transform.position = _startPos + new Vector3(0, 0, -_move);
    }
}