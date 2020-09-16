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

        //private readonly string[] _audioGameTheme = 
        //    { "Game_theme_1", "Game_theme_2", "Game_theme_3", "Game_theme_4", "Game_theme_5" };


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
            //AudioManager.Instance.PlaySound(_audioGameTheme[Random.Range(0, _audioGameTheme.Length)]);
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