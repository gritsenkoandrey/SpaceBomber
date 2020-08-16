using UnityEngine;


namespace Assets.Scripts.PoolObject
{
    /// <summary>
    /// Обертка для управления статическим классом PoolManage
    /// </summary>
    [AddComponentMenu("Pool/PoolSetup")]
    public class PoolSetup : MonoBehaviour
    {
        //структуры, где пользователь задает префаб для использования в пуле и инициализируемое количество
        [SerializeField] private PoolManager.PoolPart[] _pools;

        private void OnValidate()
        {
            for (int i = 0; i < _pools.Length; i++)
            {
                _pools[i].Name = _pools[i].Prefab.name; //присваиваем имена заранее, до инициализации
            }
        }

        private void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// инициализируем менеджер пулов
        /// </summary>
        private void Initialize()
        {
            PoolManager.Initialize(_pools);
        }
    }
}