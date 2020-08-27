using UnityEngine;


namespace Assets.Scripts.Model
{
    public sealed class SpaceshipShield : SpaceshipModel
    {
        [SerializeField] private float _currentShield = 50.0f;
        private readonly float _maxShield = 100.0f;

        public float CurrentShield
        {
            get { return _currentShield; }
            private set 
            {
                if (_currentShield >= _maxShield)
                    _currentShield = _maxShield;
                else
                    _currentShield = value;
            }
        }
    }
}