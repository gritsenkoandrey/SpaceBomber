using Assets.Scripts.TimeRemainings;
using UnityEngine;


namespace Assets.Scripts.Model
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public Transform Transform;
        protected GameObject prefab;
        private Renderer _renderer;
        private Collider _collider;
        private readonly float _timeToPool = 1.5f;
        private bool _isVisible;

        protected TimeRemaining timeRemainingReturnToPool;

        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RendererSetActive(transform);
                if (transform.childCount <= 0)
                {
                    return;
                }
                foreach (Transform t in transform)
                {
                    RendererSetActive(t);
                }
            }
        }

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Transform = GetComponent<Transform>();
            prefab = GetComponent<GameObject>();

            timeRemainingReturnToPool = new TimeRemaining(ReturnToPool, _timeToPool);
        }

        public void SetActive(bool value)
        {
            IsVisible = value;
            if (TryGetComponent(out _collider))
            {
                _collider.enabled = value;
            }
        }

        private void RendererSetActive(Transform value)
        {
            _renderer = value.gameObject.GetComponent<Renderer>();
            if (_renderer)
            {
                _renderer.enabled = _isVisible;
            }
        }

        protected void ReturnToPool()
        {
            prefab.GetComponent<PoolObject.PoolObject>().ReturnToPool();
            timeRemainingReturnToPool.RemoveTimeRemaining();
        }
    }
}