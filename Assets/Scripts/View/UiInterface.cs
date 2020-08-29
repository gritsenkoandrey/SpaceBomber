using Assets.Scripts.UI;
using UnityEngine;


namespace Assets.Scripts.View
{
    public sealed class UiInterface
    {
        private ScoreUI _scoreUI;

        private SpaceshipHealthBarUi _spaceshipHealthBarUi;
        private SpaceshipHealthTextUi _spaceshipHealthTextUi;

        private SpaceshipShieldBarUi _spaceshipShieldBarUi;
        private SpaceshipShieldTextUi _spaceshipShieldTextUi;

        private GamePauseUi _gamePauseUi;

        public ScoreUI ScoreUI
        {
            get
            {
                if (!_scoreUI)
                {
                    _scoreUI = Object.FindObjectOfType<ScoreUI>();
                }
                return _scoreUI;
            }
        }

        public SpaceshipHealthBarUi SpaceshipHealthBarUi
        {
            get
            {
                if (!_spaceshipHealthBarUi)
                {
                    _spaceshipHealthBarUi = Object.FindObjectOfType<SpaceshipHealthBarUi>();
                }
                return _spaceshipHealthBarUi;
            }
        }

        public SpaceshipHealthTextUi SpaceshipHealthTextUi
        {
            get
            {
                if (!_spaceshipHealthTextUi)
                {
                    _spaceshipHealthTextUi = Object.FindObjectOfType<SpaceshipHealthTextUi>();
                }
                return _spaceshipHealthTextUi;
            }
        }

        public SpaceshipShieldBarUi SpaceshipShieldBarUi
        {
            get
            {
                if (!_spaceshipShieldBarUi)
                {
                    _spaceshipShieldBarUi = Object.FindObjectOfType<SpaceshipShieldBarUi>();
                }
                return _spaceshipShieldBarUi;
            }
        }

        public SpaceshipShieldTextUi SpaceshipShieldTextUi
        {
            get
            {
                if (!_spaceshipShieldTextUi)
                {
                    _spaceshipShieldTextUi = Object.FindObjectOfType<SpaceshipShieldTextUi>();
                }
                return _spaceshipShieldTextUi;
            }
        }

        public GamePauseUi GamePauseUi
        {
            get
            {
                if (!_gamePauseUi)
                {
                    _gamePauseUi = Object.FindObjectOfType<GamePauseUi>();
                }
                return _gamePauseUi;
            }
        }
    }
}