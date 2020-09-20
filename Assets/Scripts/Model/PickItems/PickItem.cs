using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public abstract class PickItem : BaseObjectScene
    {
        private readonly float _minSpeed = 5.0f;
        private readonly float _maxSpeed = 25.0f;

        protected readonly string _pickRapidFireItem = "pick_item_rapid_fire_item";
        protected readonly string _pickShieldItem = "pick_item_shield";
        protected readonly string[] _pickEnergyItem =
            { "pick_item_energy_01", "pick_item_energy_02" };
        protected readonly string _pickHealthItem = "pick_item_health";

        protected SpaceshipFire spaceshipFire;
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