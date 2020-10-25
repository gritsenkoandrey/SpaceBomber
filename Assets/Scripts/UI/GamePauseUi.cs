using UnityEngine;
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

        [SerializeField] private ButtonUi _resume = null;
        [SerializeField] private ButtonUi _mainMenu = null;
        [SerializeField] private ButtonUi _quit = null;


        protected override void Awake()
        {
            base.Awake();

            pause = mixer.FindSnapshot(pausedSnapshot);
            unPause = mixer.FindSnapshot(unPausedSnapshot);

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
                pause.TransitionTo(timeToReach);
                pausePanel.SetActive(true);
                gamePanel.SetActive(false);
                ship.IsFire = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = timeOn;
                unPause.TransitionTo(timeToReach);
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
            unPause.TransitionTo(timeToReach);
            EnemyManager.Cleanup();
            ServiceLocator.Cleanup();
            ServiceLocatorMonoBehaviour.Cleanup();
            SceneManager.LoadScene(_gameScene);
        }
    }
}