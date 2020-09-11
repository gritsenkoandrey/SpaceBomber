using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public abstract class PickItem : BaseObjectScene
    {
        private readonly float _minSpeed = 5.0f;
        private readonly float _maxSpeed = 25.0f;

        protected readonly string _pickRapidFireItem = "sci-fi_shield_device_power_up_01";
        protected readonly string _pickShieldItem = "sci-fi_power_up_10";
        protected readonly string[] _pickEnergyItem = 
            { "sci-fi_shield_power_on_impact_01", "sci-fi_shield_power_on_impact_02" };
        protected readonly string _pickHealthItem = "sci-fi_power_up_object_01";

        protected SpaceshipFire spaceshipFire;
        protected SpaceshipMove spaceshipMove;
        protected SpaceshipHealth spaceshipHealth;
        protected SpaceshipShield spaceshipShield;
        protected ScrollBackground scrollBackground;

        protected override void Awake()
        {
            base.Awake();
            Move();
            scrollBackground = FindObjectOfType<ScrollBackground>();
        }

        private void Move()
        {
            Rigidbody.velocity = new Vector3(0, 0, -Random.Range(_minSpeed, _maxSpeed));
        }
    }
}