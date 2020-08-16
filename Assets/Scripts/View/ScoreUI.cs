using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public sealed class ScoreUI : BaseObjectScene
    {
        private int _score;
        private Text _scoreText;
        internal static ScoreUI instance;

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        protected override void Awake()
        {
            base.Awake();
            _scoreText = GetComponent<Text>();
            _scoreText.color = Color.green;
            instance = this;
        }

        private void Update()
        {
            _scoreText.text = $"Score: {_score}";
        }

        #region Event

        //private void OnEnable()
        //{
        //    foreach (var enemy in _enemies)
        //    {
        //        enemy.OnPointChange += UpdatePoint;
        //    }
        //    foreach (var asteroid in _asteroids)
        //    {
        //        asteroid.OnPointChange += UpdatePoint;
        //    }
        //}

        //private void OnDisable()
        //{
        //    foreach (var enemy in _enemies)
        //    {
        //        enemy.OnPointChange -= UpdatePoint;
        //    }
        //    foreach (var asteroid in _asteroids)
        //    {
        //        asteroid.OnPointChange -= UpdatePoint;
        //    }
        //}

        //private void UpdatePoint()
        //{
        //    Score += 10;
        //}

        #endregion
    }
}