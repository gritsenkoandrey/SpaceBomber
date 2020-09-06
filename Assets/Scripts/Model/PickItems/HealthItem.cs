using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public sealed class HealthItem : PickItem
    {
        [SerializeField] private float _gainHealth = 50.0f;

        private void OnTriggerEnter(Collider other)
        {
            spaceshipHealth = other.GetComponent<SpaceshipHealth>();

            if (spaceshipHealth)
            {
                this.gameObject.GetComponent<PoolObject.PoolObject>().ReturnToPool();
                spaceshipHealth.HealthTaken(_gainHealth);
            }
        }
    }
}