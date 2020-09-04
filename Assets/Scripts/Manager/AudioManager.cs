using Assets.Scripts.Model;
using UnityEngine;


namespace Assets.Scripts.Manager
{
    //todo доработать класс через пулобъектов
    public sealed class AudioManager : BaseObjectScene
    {
        private AudioSource _source;
        private static AudioClip _clip;
        public static AudioManager Instance;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            _source = FindObjectOfType<AudioSource>();
        }

        //private void Update()
        //{
        //    if (!_source.isPlaying && _clip != null)
        //    {
        //        Resources.UnloadAsset(_clip);
        //        _clip = null;
        //    }
        //}

        public void PlaySound(string resourceName)
        {
            _clip = Resources.Load(resourceName) as AudioClip;
            _source.PlayOneShot(_clip);
        }
    }
}