using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;


namespace Assets.Scripts.UI
{
    public sealed class GamePauseUi : BaseObjectUi
    {
        private bool _isPaused = false;
        private readonly byte _timeOn = 1;
        private readonly byte _timeOff = 0;
        private readonly string _pausedSnapshot = "Paused";
        private readonly string _unPausedSnapshot = "UnPaused";
        private readonly float _timeToReach = 0.0001f;

        [SerializeField] private ButtonUi _resume;
        [SerializeField] private ButtonUi _mainMenu;
        [SerializeField] private ButtonUi _quit;

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioMixerGroup _mixerGroup;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private GameObject _pausePanel;

        private AudioMixerSnapshot _pause;
        private AudioMixerSnapshot _unPause;

        private SpaceshipFire _ship;

        private void Awake()
        {
            _pause = _mixer.FindSnapshot(_pausedSnapshot);
            _unPause = _mixer.FindSnapshot(_unPausedSnapshot);
            _ship = Object.FindObjectOfType<SpaceshipFire>();

            _resume.GetButton.onClick.AddListener(delegate { Pause(); });
            _quit.GetButton.onClick.AddListener(delegate { QuitGame(); });
        }

        //todo доработать состояние корабля во время паузы
        public void Pause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Time.timeScale = _timeOff;
                _pause.TransitionTo(_timeToReach);
                _pausePanel.SetActive(true);
                _gamePanel.SetActive(false);
                _ship.IsFire = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = _timeOn;
                _unPause.TransitionTo(_timeToReach);
                _pausePanel.SetActive(false);
                _gamePanel.SetActive(true);
                _ship.IsFire = true;
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

        public void StartCondition()
        {
            _gamePanel.SetActive(true);
            _pausePanel.SetActive(false);
        }
    }
}