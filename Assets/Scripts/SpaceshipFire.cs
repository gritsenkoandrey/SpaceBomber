using Assets.Scripts;
using UnityEngine;


public sealed class SpaceshipFire : SpaceshipModel
{
    private readonly int _leftButton = (int)MouseButton.LeftButton;
    private readonly int _rightButton = (int)MouseButton.RightButton;

    private float _forceAmmunition = 5000.0f;

    private Bullet _bullet;

    [SerializeField] private Bullet _bulletBlue;
    [SerializeField] private Bullet _bulletYellow;

    [SerializeField] private Transform _gunOne;
    [SerializeField] private Transform _gunTwo;
    [SerializeField] private Transform _gunThree;
    [SerializeField] private Transform _gunFour;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Input.GetMouseButtonDown(_leftButton))
        {
            _bullet = Instantiate(_bulletBlue, _gunOne.position, Quaternion.identity);
            _bullet.AddForce(_gunOne.forward * _forceAmmunition);

            _bullet = Instantiate(_bulletBlue, _gunTwo.position, Quaternion.identity);
            _bullet.AddForce(_gunOne.forward * _forceAmmunition);
        }

        if (Input.GetMouseButtonDown(_rightButton))
        {
            _bullet = Instantiate(_bulletYellow, _gunThree.position, Quaternion.identity);
            _bullet.AddForce(_gunThree.forward * _forceAmmunition);

            _bullet = Instantiate(_bulletYellow, _gunFour.position, Quaternion.identity);
            _bullet.AddForce(_gunFour.forward * _forceAmmunition);
        }
    }
}