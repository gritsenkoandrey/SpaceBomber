using Assets.Scripts.Manager;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipFire : SpaceshipModel
{
    private readonly float _forceAmmunition = 100.0f;

    private float _delay;
    private float _maxDelay = 0.5f;
    private float _minDelay = 0.1f;
    private float _nextLaunchTime = 1.0f;

    private readonly string _bulletBlue = "BulletBlue";

    private bool _isFire = true;

    [SerializeField] private Transform _gunOne;
    [SerializeField] private Transform _gunTwo;
    [SerializeField] private Transform _gunThree;
    [SerializeField] private Transform _gunFour;

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
    }

    public void FireFirstWeapon()
    {
        if (_isFire)
        {
            if (Time.time > _nextLaunchTime)
            {
                prefab = PoolManager.GetObject(_bulletBlue, _gunOne.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                AudioManager.Instance.PlaySound("Laser20");

                prefab = PoolManager.GetObject(_bulletBlue, _gunTwo.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                AudioManager.Instance.PlaySound("Laser20");

                _nextLaunchTime = Time.time + _delay;
            }
        }
    }

    public void FireSecondWeapon()
    {
        if (_isFire)
        {
            if (Time.time > _nextLaunchTime)
            {
                prefab = PoolManager.GetObject(_bulletBlue, _gunThree.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                AudioManager.Instance.PlaySound("Laser15");

                prefab = PoolManager.GetObject(_bulletBlue, _gunFour.position, Quaternion.identity);
                prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);
                AudioManager.Instance.PlaySound("Laser15");

                _nextLaunchTime = Time.time + _delay;
            }
        }
    }

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
}