using Assets.Scripts.Model;
using UnityEngine;


namespace Assets.Scripts.UI
{
    public abstract class BaseObjectUi : BaseObjectScene
    {
        [SerializeField] protected GameObject gamePanel;
        [SerializeField] protected GameObject pausePanel;
        [SerializeField] protected GameObject mainMenuPanel;
        [SerializeField] private GameObject[] _background;

        protected readonly byte timeOn = 1;
        protected readonly byte timeOff = 0;

        protected SpaceshipFire ship;

        protected override void Awake()
        {
            base.Awake();
            ship = Object.FindObjectOfType<SpaceshipFire>();
        }

        /// <summary>
        /// Управление GamePanel, PausePanel, MainMenuPanel, SettingsPanel при старте игры.
        /// </summary>
        public void StartCondition()
        {
            mainMenuPanel.SetActive(true);
            gamePanel.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = timeOff;
            ship.IsFire = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _background[Random.Range(0, _background.Length)].SetActive(true);
        }
    }
}