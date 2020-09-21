using UnityEngine;


namespace Assets.Scripts.UI
{
    public sealed class SettingsMenuUi : BaseObjectUi
    {
        [SerializeField] private ButtonUi _back;

        protected override void Awake()
        {
            base.Awake();

            _back.GetButton.onClick.AddListener(delegate { Back(); });
        }

        private void Back()
        {
            gamePanel.SetActive(true);
            mainMenuPanel.SetActive(true);
            pausePanel.SetActive(false);
            settingsPanel.SetActive(false);
        }
    }
}