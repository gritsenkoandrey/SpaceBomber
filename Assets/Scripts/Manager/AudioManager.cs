using Assets.Scripts.Singleton;
using UnityEngine;


namespace Assets.Scripts.Manager
{
    //todo доработать класс через пулобъектов
    public sealed class AudioManager /*: SingletonAsComponent<AudioManager>*/ : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        private AudioClip _clip;

        public static AudioManager Instance;
        //public static AudioManager Instance
        //{
        //    get { return ((AudioManager)instance); }
        //    set { instance = value; }
        //}

        private void Start()
        {
            //_source = Object.FindObjectOfType<AudioSource>();
            Instance = this;
        }

        private void Update()
        {
            if (!_source.isPlaying && _clip != null)
            {
                Resources.UnloadAsset(_clip);
                _clip = null;
            }
        }

        public void PlaySound(string resourceName)
        {
            _clip = Resources.Load(resourceName) as AudioClip;
            _source.PlayOneShot(_clip);
        }
    }
}