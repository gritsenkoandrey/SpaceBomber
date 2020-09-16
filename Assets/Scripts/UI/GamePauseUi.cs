using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using UnityEngine.SceneManagement;
using Assets.Scripts.Model;
using Assets.Scripts.ServiceLocators;


namespace Assets.Scripts.UI
{
    public sealed class GamePauseUi : BaseObjectUi
    {
        private bool _isPaused = false;
        private readonly byte _gameScene = 0;
        private readonly string _pausedSnapshot = "Paused";
        private readonly string _unPausedSnapshot = "UnPaused";
        private readonly float _timeToReach = 0.0001f;

        [SerializeField] private ButtonUi _resume;
        [SerializeField] private ButtonUi _mainMenu;
        [SerializeField] private ButtonUi _quit;

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioMixerGroup _mixerGroup;

        private AudioMixerSnapshot _pause;
        private AudioMixerSnapshot _unPause;

        protected override void Awake()
        {
            base.Awake();

            _pause = _mixer.FindSnapshot(_pausedSnapshot);
            _unPause = _mixer.FindSnapshot(_unPausedSnapshot);

            _resume.GetButton.onClick.AddListener(delegate { Pause(); });
            _mainMenu.GetButton.onClick.AddListener(delegate { RestartGame(); });
            _quit.GetButton.onClick.AddListener(delegate { QuitGame(); });
        }

        public void Pause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Time.timeScale = timeOff;
                _pause.TransitionTo(_timeToReach);
                pausePanel.SetActive(true);
                gamePanel.SetActive(false);
                ship.IsFire = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = timeOn;
                _unPause.TransitionTo(_timeToReach);
                pausePanel.SetActive(false);
                gamePanel.SetActive(true);
                ship.IsFire = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void QuitGame()
        {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }

        // доработать рестрат уровня.
        private void RestartGame()
        {
            Time.timeScale = timeOn;
            _unPause.TransitionTo(_timeToReach);
            EnemyManager.Cleanup();
            ServiceLocator.Cleanup();
            ServiceLocatorMonoBehaviour.Cleanup();
            SceneManager.LoadScene(_gameScene);
        }
    }
}