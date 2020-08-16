using UnityEngine;


public abstract class SpaceshipModel : MonoBehaviour
{
    protected Rigidbody ship;
    protected GameObject prefab;
    protected Bullet bullet;

    protected void Start()
    {
        ship = GetComponent<Rigidbody>();
    }
}