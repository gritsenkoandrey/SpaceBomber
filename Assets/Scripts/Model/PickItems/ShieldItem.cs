using Assets.Scripts.Manager;
using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    public sealed class ShieldItem : PickItem
    {
        [SerializeField] private float _gainShield = 20.0f;

        private void OnTriggerEnter(Collider other)
        {
            spaceshipShield = other.GetComponentInChildren<SpaceshipShield>();

            if (spaceshipShield)
            {
                this.gameObject.GetComponent<PoolObject.PoolObject>().ReturnToPool();
                spaceshipShield.ShieldTaken(_gainShield);
                AudioManager.Instance.PlaySound(_pickShieldItem);
            }
        }
    }
}