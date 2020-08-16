using UnityEngine;


namespace Assets.Scripts.PoolObject
{
    [AddComponentMenu("Pool/PoolObject")]
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}