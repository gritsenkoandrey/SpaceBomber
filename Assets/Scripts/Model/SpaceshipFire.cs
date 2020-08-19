using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipFire : SpaceshipModel
{
    private readonly float _forceAmmunition = 100.0f;

    private float _delay = 0.5f;
    private float _nextLaunchTime = 1.0f;

    private readonly string _bulletBlue = "BulletBlue";
    private readonly string _bulletYellow = "BulletYellow";

    [SerializeField] private Transform _gunOne;
    [SerializeField] private Transform _gunTwo;
    [SerializeField] private Transform _gunThree;
    [SerializeField] private Transform _gunFour;

    public void FireFirstWeapon()
    {
        if (Time.time > _nextLaunchTime)
        {
            prefab = PoolManager.GetObject(_bulletBlue, _gunOne.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);

            prefab = PoolManager.GetObject(_bulletBlue, _gunTwo.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);

            _nextLaunchTime = Time.time + _delay;
        }
    }

    public void FireSecondWeapon()
    {
        if (Time.time > _nextLaunchTime)
        {
            prefab = PoolManager.GetObject(_bulletYellow, _gunThree.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);

            prefab = PoolManager.GetObject(_bulletYellow, _gunFour.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().Velocity(_forceAmmunition);

            _nextLaunchTime = Time.time + _delay;
        }
    }
}