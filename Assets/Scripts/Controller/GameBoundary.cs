using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class GameBoundary : MonoBehaviour
{
    private Bullet _bullet;
    private SpaceshipEnemy _enemy;

    private void OnTriggerExit(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _enemy = other.gameObject.GetComponent<SpaceshipEnemy>();

        if (_bullet)
        {
            _bullet.GetComponent<PoolObject>().ReturnToPool();
        }
        else if (_enemy)
        {
            _enemy.GetComponent<PoolObject>().ReturnToPool();
        }
        else
        {
            return;
        }
    }
}