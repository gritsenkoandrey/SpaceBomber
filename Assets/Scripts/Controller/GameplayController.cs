using Assets.Scripts.Interface;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    /// <summary>
    /// GameplayController управляет ScrollBackground, ScoreUI, GamePauseUi
    /// </summary>
    public sealed class GameplayController : BaseController, IExecute, IInitialization
    {
        private ScrollBackground _scrollBackground;

        public void Initialization()
        {
            _scrollBackground = Object.FindObjectOfType<ScrollBackground>();
            uiInterface.GamePauseUi.StartCondition();
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