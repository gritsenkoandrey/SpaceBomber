﻿using Assets.Scripts.Manager;
using Assets.Scripts.PoolObject;
using Assets.Scripts.TimeRemainings;
using UnityEngine;


public sealed class SpaceshipFire : SpaceshipModel
{
    private readonly float _forceAmmunition = 100.0f;

    private float _delay;
    private readonly float _maxDelay = 0.4f;
    private readonly float _minDelay = 0.1f;
    private readonly float _nextLaunchTime = 0.1f;

    private TimeRemaining _timeRemaining;

    private readonly string _bulletBluePrefab = "BulletBlue";
    private readonly string _audioBulletOne = "laser_spaceship_01";
    private readonly string _audioBulletTwo = "laser_spaceship_02";

    private bool _isFire = true;
    private bool _isReady = true;

    [SerializeField] private Transform _gunOne = null;
    [SerializeField] private Transform _gunTwo = null;
    [SerializeField] private Transform _gunThree = null;
    [SerializeField] private Transform _gunFour = null;

    public bool IsFire
    {
        get { return _isFire; }
        set { _isFire = value; }
    }

    public float Delay
    {
        get { return _delay; }
        private set { _delay = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _delay = _maxDelay;
        _timeRemaining = new TimeRemaining(ReadyShoot, _nextLaunchTime);
    }

    public void FireFirstWeapon()
    {
        if (_isFire && _isReady)
        {
            prefab = PoolManager.GetObject(_bulletBluePrefab, _gunOne.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
            AudioManager.Instance.PlaySound(_audioBulletOne);

            prefab = PoolManager.GetObject(_bulletBluePrefab, _gunTwo.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
            AudioManager.Instance.PlaySound(_audioBulletOne);

            _isReady = false;
            _timeRemaining.AddTimeRemaining();
            _timeRemaining.CurrentTime += Delay;
        }
    }

    public void FireSecondWeapon()
    {
        if (_isFire && _isReady)
        {
            prefab = PoolManager.GetObject(_bulletBluePrefab, _gunThree.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
            AudioManager.Instance.PlaySound(_audioBulletTwo);

            prefab = PoolManager.GetObject(_bulletBluePrefab, _gunFour.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
            AudioManager.Instance.PlaySound(_audioBulletTwo);

            _isReady = false;
            _timeRemaining.AddTimeRemaining();
            _timeRemaining.CurrentTime += Delay;
        }
    }
    // todo доработать метод, чтобы при подборе 
    // одинакового предмета время бафа отсчитывалось с последнего предмета.

    /// <summary>
    /// Изменить задержу в выстрелах на время.
    /// </summary>
    /// <param name="delay">Показатель задержки</param>
    /// <param name="time">Время действия эффекта</param>
    public void ChangeDelay(float delay, float time)
    {
        Delay -= delay;
        if (Delay < _minDelay)
        {
            Delay = _minDelay;
        }
        Invoke(nameof(ReturnToNormalDelay), time);
    }

    private void ReturnToNormalDelay()
    {
        Delay = _maxDelay;
    }

    private void ReadyShoot()
    {
        _isReady = true;
    }
}