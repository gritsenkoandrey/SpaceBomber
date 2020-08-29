using UnityEngine;


namespace Assets.Scripts.Model
{
    public sealed class SpaceshipShield : SpaceshipModel
    {
        [SerializeField] private float _maxShield = 100.0f;
        private float _currentShield;
        private readonly byte _minShield = 0;
        private readonly int _maxPercent = 100;

        public float CurrentShield
        {
            get 
            {
                if (_currentShield >= _maxShield)
                {
                    _currentShield = _maxShield;
                }
                return _currentShield;
            }
            set { _currentShield = value; }
        }

        public float FillShield
        {
            get { return _currentShield / _maxShield; }
        }

        public float PercentShield
        {
            get
            {
                if (_currentShield <= _minShield)
                {
                    return _minShield;
                }
                else
                {
                    return _currentShield / _maxShield * _maxPercent;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _currentShield = _maxShield;
        }
    }
}