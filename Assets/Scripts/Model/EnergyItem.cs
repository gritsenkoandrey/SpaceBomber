using UnityEngine;


namespace Assets.Scripts.Model
{
    public sealed class EnergyItem : PickItem
    {
        [SerializeField] private float _energySpeed = 50.0f;
        [SerializeField] private float _timeEffect = 5.0f;
        private SpaceshipMove _ship;

        private void OnTriggerEnter(Collider other)
        {
            _ship = other.GetComponent<SpaceshipMove>();

            if (_ship)
            {
                //this.gameObject.GetComponent<PoolObject.PoolObject>().ReturnToPool();
                this.gameObject.SetActive(false);
                _ship.PickEnergyItem(_energySpeed, _timeEffect);
            }
        }
    }
}