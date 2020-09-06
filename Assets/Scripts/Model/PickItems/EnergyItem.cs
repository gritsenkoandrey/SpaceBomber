using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public sealed class EnergyItem : PickItem
    {
        [SerializeField] private float _energySpeed = 50.0f;
        [SerializeField] private float _timeEffect = 5.0f;

        private void OnTriggerEnter(Collider other)
        {
            spaceshipMove = other.GetComponent<SpaceshipMove>();

            if (spaceshipMove)
            {
                this.gameObject.GetComponent<PoolObject.PoolObject>().ReturnToPool();
                spaceshipMove.PickEnergyItem(_energySpeed, _timeEffect);
            }
        }
    }
}