using Assets.Scripts.PoolObject;
using System.Collections;
using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private int _health = 100;
    private readonly string _explosionShip = "ShipExplosion";
    private readonly float _returnToPool = 1.5f;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet)
        {
            Health -= bullet.Damage;
            bullet.GetComponent<PoolObject>().ReturnToPool();
            if (_health <= 0)
            {
                prefab = PoolManager.GetObject(_explosionShip,
                    this.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(ReturnToPool(prefab));
                // todo наш корабль
                Destroy(this.gameObject);
            }
        }
        else
        {
            return;
        }
    }

    private IEnumerator ReturnToPool(GameObject obj)
    {
        yield return new WaitForSeconds(_returnToPool);
        obj.GetComponent<PoolObject>().ReturnToPool();
    }
}