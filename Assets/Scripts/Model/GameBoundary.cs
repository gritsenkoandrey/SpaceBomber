using Assets.Scripts.Model;
using Assets.Scripts.PoolObject;
using UnityEngine;


public sealed class GameBoundary : BaseObjectScene
{
    private Bullet _bullet;
    private SpaceshipEnemy _enemy;
    private AsteroidModel _asteroid;

    private void OnTriggerExit(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();
        _enemy = other.gameObject.GetComponent<SpaceshipEnemy>();
        _asteroid = other.gameObject.GetComponent<AsteroidModel>();

        if (_bullet)
        {
            _bullet.GetComponent<PoolObject>().ReturnToPool();
        }
        else if (_enemy)
        {
            _enemy.GetComponent<PoolObject>().ReturnToPool();
            EnemyManager.RemoveEnemieToList(_enemy);
        }
        else if (_asteroid)
        {
            _asteroid.GetComponent<PoolObject>().ReturnToPool();
        }
        else
        {
            return;
        }
    }
}