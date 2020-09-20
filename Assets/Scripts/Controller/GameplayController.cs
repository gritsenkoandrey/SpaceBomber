using Assets.Scripts.Interface;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    /// <summary>
    /// GameplayController управляет ScrollBackground, ScoreUI, GamePauseUi, MainMenuUi
    /// </summary>
    public sealed class GameplayController : BaseController, IExecute, IInitialization
    {
        private ScrollBackground _scrollBackground;

        public void Initialization()
        {
            uiInterface.MainMenuUi.StartCondition();
            _scrollBackground = Object.FindObjectOfType<ScrollBackground>();
        }

        public void Execute()
        {
            uiInterface.ScoreUI.ShowScore();
            _scrollBackground.Move();
        }

        public void Pause()
        {
            uiInterface.GamePauseUi.Pause();
        }
    }
}