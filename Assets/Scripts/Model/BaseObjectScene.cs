using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Model
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        new protected Rigidbody rigidbody;
        new protected Transform transform;
        protected GameObject prefab;
        private Color _color;
        private readonly float _returnToPool = 1.5f;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            transform = GetComponent<Transform>();
            prefab = GetComponent<GameObject>();
        }

        protected IEnumerator ReturnToPool(GameObject obj)
        {
            yield return new WaitForSeconds(_returnToPool);
            obj.GetComponent<PoolObject.PoolObject>().ReturnToPool();
        }
    }
}