using Assets.Scripts;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class SpaceshipFire : SpaceshipModel, IFire
{
    private readonly int _leftButton = (int)MouseButton.LeftButton;
    private readonly int _rightButton = (int)MouseButton.RightButton;
    private readonly float _forceAmmunition = 5000.0f;
    private readonly string _bulletBlue = "BulletBlue";
    private readonly string _bulletYellow = "BulletYellow";

    [SerializeField] private Transform _gunOne;
    [SerializeField] private Transform _gunTwo;
    [SerializeField] private Transform _gunThree;
    [SerializeField] private Transform _gunFour;

    private void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if (Input.GetMouseButtonDown(_leftButton))
        {
            prefab = PoolManager.GetObject(_bulletBlue, _gunOne.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().AddForce(_gunOne.forward * _forceAmmunition);

            prefab = PoolManager.GetObject(_bulletBlue, _gunTwo.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().AddForce(_gunTwo.forward * _forceAmmunition);
        }

        if (Input.GetMouseButtonDown(_rightButton))
        {
            prefab = PoolManager.GetObject(_bulletYellow, _gunThree.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().AddForce(_gunThree.forward * _forceAmmunition);

            prefab = PoolManager.GetObject(_bulletYellow, _gunFour.position, Quaternion.identity);
            prefab.GetComponent<Bullet>().AddForce(_gunFour.forward * _forceAmmunition);
        }
    }
}