using Assets.Scripts.Interface;
using Assets.Scripts.Model.PickItems;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    /// <summary>
    /// GameplayController управляет ScrollBackground, ScoreUI, GamePauseUi
    /// </summary>
    public sealed class GameplayController : BaseController, IExecute, IInitialization
    {
        private ScrollBackground _scrollBackground;
        private RapidFireItem _rapidFireItem;

        public void Initialization()
        {
            _scrollBackground = Object.FindObjectOfType<ScrollBackground>();
            uiInterface.GamePauseUi.StartCondition();
        }

        public void Execute()
        {
            uiInterface.ScoreUI.ShowScore();
            uiInterface.RapidFireItemUi.Text = Time.time;
            _scrollBackground.Move();
        }

        public void Pause()
        {
            uiInterface.GamePauseUi.Pause();
        }
    }
}