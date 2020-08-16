using UnityEngine;


namespace Assets.Scripts
{
    public abstract class SpaceshipEnemyModel : MonoBehaviour
    {
        protected Rigidbody ship;
        protected Bullet bullet;
        protected SpaceshipModel spaceship;
        protected GameObject prefab;
    }
}