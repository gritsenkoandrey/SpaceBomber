using UnityEngine;


public sealed class SpaceshipHealth : SpaceshipModel
{
    [SerializeField] private int _health = 100;
    [SerializeField] private Object _explosion;

    private Bullet _bullet;

    public int Health
    {
        get { return _health; }
        set { _health = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        _bullet = other.gameObject.GetComponent<Bullet>();

        if (_bullet)
        {
            Health -= 10;
            Destroy(_bullet.gameObject);

            if (_health <= 0)
            {
                Instantiate(_explosion, this.gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else
        {
            return;
        }
    }
}