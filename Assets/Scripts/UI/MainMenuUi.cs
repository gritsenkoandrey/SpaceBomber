using Assets.Scripts.Manager;
using UnityEditor;
using UnityEngine;


namespace Assets.Scripts.UI
{
    public sealed class MainMenuUi : BaseObjectUi
    {
        [SerializeField] private ButtonUi _start;
        [SerializeField] private ButtonUi _settings;
        [SerializeField] private ButtonUi _quit;
        [SerializeField] private GameObject[] _background;

        private readonly string _audioMainMenu = "MainMenu_theme";

        protected override void Awake()
        {
            base.Awake();

            pause = mixer.FindSnapshot(pausedSnapshot);
            unPause = mixer.FindSnapshot(unPausedSnapshot);

            _start.GetButton.onClick.AddListener(delegate { StartGame(); });
            _settings.GetButton.onClick.AddListener(delegate { SettingsGame(); });
            _quit.GetButton.onClick.AddListener(delegate { QuitGame(); });
        }

        private void StartGame()
        {
            mainMenuPanel.SetActive(false);
            gamePanel.SetActive(true);
            pausePanel.SetActive(false);
            Time.timeScale = timeOn;
            ship.IsFire = true; // активируем возможность корабля стрелять
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AudioManager.Instance.StopSound(); // выгружаем из ресурсов музыку главного меню
            unPause.TransitionTo(timeToReach); // активируем музыку в игре
        }

        private void SettingsGame()
        {
        }

        private void QuitGame()
        {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
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
            ship.IsFire = false; // деактивируем возможность стрельбы карабля в главном меню
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _background[Random.Range(0, _background.Length)].SetActive(true);
            pause.TransitionTo(timeToReach); // пауза музыки в игре
            AudioManager.Instance.PlaySound(_audioMainMenu); // запуск музыки в главном меню
        }
    }
}