using UnityEngine;


namespace Assets.Scripts.PoolObject
{
    [AddComponentMenu("Pool/PoolObject")]
    public sealed class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}