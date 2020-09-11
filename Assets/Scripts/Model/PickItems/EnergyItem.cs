using Assets.Scripts.Manager;
using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public sealed class EnergyItem : PickItem
    {
        [SerializeField] private float _gainSpeed = 50.0f;
        [SerializeField] private float _timeLeft = 5.0f;

        private void OnTriggerEnter(Collider other)
        {
            spaceshipMove = other.GetComponent<SpaceshipMove>();

            if (spaceshipMove)
            {
                this.gameObject.GetComponent<PoolObject.PoolObject>().ReturnToPool();
                spaceshipMove.PickEnergyItem(_gainSpeed, _timeLeft);
                scrollBackground.PickEnergyItem(_gainSpeed, _timeLeft);
                AudioManager.Instance.PlaySound(_pickEnergyItem[Random.Range(0, _pickEnergyItem.Length)]);
            }
        }
    }
}