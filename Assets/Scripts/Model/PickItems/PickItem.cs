using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public abstract class PickItem : BaseObjectScene
    {
        private readonly float _minSpeed = 5.0f;
        private readonly float _maxSpeed = 25.0f;

        protected SpaceshipMove spaceshipMove;
        protected SpaceshipHealth spaceshipHealth;
        protected SpaceshipShield spaceshipShield;

        protected override void Awake()
        {
            base.Awake();
            Move();
        }

        private void Move()
        {
            Rigidbody.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
        }
    }
}