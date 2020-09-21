using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.Audio;


namespace Assets.Scripts.UI
{
    public abstract class BaseObjectUi : BaseObjectScene
    {
        [SerializeField] protected GameObject gamePanel;
        [SerializeField] protected GameObject pausePanel;
        [SerializeField] protected GameObject mainMenuPanel;
        [SerializeField] protected GameObject settingsPanel;

        [SerializeField] protected AudioMixer mixer;
        protected AudioMixerSnapshot pause;
        protected AudioMixerSnapshot unPause;

        protected readonly byte timeOn = 1;
        protected readonly byte timeOff = 0;
        protected readonly float timeToReach = 0.0001f;

        protected readonly string pausedSnapshot = "Paused";
        protected readonly string unPausedSnapshot = "UnPaused";

        protected SpaceshipFire ship;

        protected override void Awake()
        {
            base.Awake();
            ship = Object.FindObjectOfType<SpaceshipFire>();
        }
    }
}