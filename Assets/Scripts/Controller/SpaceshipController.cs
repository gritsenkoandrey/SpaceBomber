using Assets.Scripts.Interface;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class SpaceshipController : BaseController, IExecute, IInitialization
    {
        private IMove _move;
        private SpaceshipFire _fire;
        private SpaceshipHealth _health;

        public void Initialization()
        {
            _fire = Object.FindObjectOfType<SpaceshipFire>();
            _move = Object.FindObjectOfType<SpaceshipMove>();
            _health = Object.FindObjectOfType<SpaceshipHealth>();

            uiInterface.SpaceshipHealthBarUi.SetActive(true);
            uiInterface.SpaceshipHealthTextUi.SetActive(true);
            uiInterface.SpaceshipHealthBarUi.SetColor(Color.green);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }
            else
            {
                _move.Move();
                uiInterface.SpaceshipHealthBarUi.Fill = _health.FillHealth;
                uiInterface.SpaceshipHealthTextUi.Text = _health.PercentHealth;

                if (_health.CurrentHealth < _health.AverageHealt)
                {
                    uiInterface.SpaceshipHealthBarUi.SetColor(Color.red);
                }
                if (_health.CurrentHealth <= 0)
                {
                    //todo
                    return;
                }
            }
        }

        public void FireFirstWeapon()
        {
            _fire.FireFirstWeapon();
        }

        public void FireSecondWeapon()
        {
            _fire.FireSecondWeapon();
        }
    }
}