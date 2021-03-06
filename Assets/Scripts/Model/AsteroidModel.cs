﻿using Assets.Scripts;
using Assets.Scripts.Interface;
using Assets.Scripts.Manager;
using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using Assets.Scripts.TimeRemainings;
using UnityEngine;


public sealed class AsteroidModel : BaseObjectScene, IMove
{
    [SerializeField] private int _collisionDamage = 20;

    private float _rotation;
    private readonly float _minRotation = 5.0f;
    private readonly float _maxRotation = 20.0f;

    private readonly float _minSpeed = 5.0f;
    private readonly float _maxSpeed = 25.0f;

    private readonly byte _points = 5;

    private readonly string _explosionAsteroidPrefab = "AsteroidExplosion";
    private readonly string _explosionAsteroidSound = "explosion_asteroid";
    private Bullet _bullet;

    public int CollisionDamage
    {
        get { return _collisionDamage; }
        private set { _collisionDamage = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        if (_bullet)
        {
            prefab = PoolManager.GetObject
                (_explosionAsteroidPrefab, this.gameObject.transform.position, Quaternion.identity);
            timeRemainingReturnToPool.AddTimeRemaining();
            this.gameObject.GetComponent<PoolObject>().ReturnToPool();
            _bullet.GetComponent<PoolObject>().ReturnToPool();
            AudioManager.Instance.PlaySound(_explosionAsteroidSound);
            ScoreUI.instance.Score += _points;
        }
    }

    public void Move()
    {
        _rotation = Random.Range(_minRotation, _maxRotation);
        // случайное вращение астеройда
        Rigidbody.angularVelocity = Random.insideUnitSphere * _rotation;
        Rigidbody.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
    }
}