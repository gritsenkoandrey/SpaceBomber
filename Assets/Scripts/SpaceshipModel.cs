using UnityEngine;


public abstract class SpaceshipModel : MonoBehaviour
{
    protected Rigidbody _ship;

    protected void Start()
    {
        _ship = GetComponent<Rigidbody>();
    }
}