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

        protected override void Awake()
        {
            base.Awake();

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
            ship.IsFire = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AudioManager.Instance.PlaySound(audioGameTheme[Random.Range(0, audioGameTheme.Length)]);
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
    }
}