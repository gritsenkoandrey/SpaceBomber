using Assets.Scripts.Manager;
using UnityEngine;


namespace Assets.Scripts.Model.PickItems
{
    /// <summary>
    /// Класс который отвечает за уменьшение перезарядки.
    /// </summary>
    public sealed class RapidFireItem : PickItem
    {
        [SerializeField] private float _timeEffect = 10.0f;
        private float _reduceDelay = 0.4f;

        //public float TimeEffect
        //{
        //    get { return _timeEffect; }
        //    private set { _timeEffect = value; }
        //}

        private void OnTriggerEnter(Collider other)
        {
            spaceshipFire = other.GetComponent<SpaceshipFire>();

            if (spaceshipFire)
            {
                this.gameObject.GetComponent<PoolObject.PoolObject>().ReturnToPool();
                spaceshipFire.ChangeDelay(_reduceDelay, _timeEffect);
                AudioManager.Instance.PlaySound(_pickRapidFireItem);
            }
        }
    }
}