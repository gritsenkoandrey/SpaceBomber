using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class SpaceshipController : BaseController, IExecute, IInitialization
    {
        private IMove _move;
        private SpaceshipFire _fire;
        private SpaceshipHealth _health;
        private SpaceshipShield _shield;

        public void Initialization()
        {
            _fire = Object.FindObjectOfType<SpaceshipFire>();
            _move = Object.FindObjectOfType<SpaceshipMove>();
            _health = Object.FindObjectOfType<SpaceshipHealth>();
            _shield = Object.FindObjectOfType<SpaceshipShield>();

            uiInterface.SpaceshipHealthBarUi.SetActive(true);
            uiInterface.SpaceshipHealthTextUi.SetActive(true);
            uiInterface.SpaceshipHealthBarUi.SetColor(Color.green);

            uiInterface.SpaceshipShieldBarUi.SetActive(true);
            uiInterface.SpaceshipShieldTextUi.SetActive(true);
            uiInterface.SpaceshipShieldBarUi.SetColor(Color.blue);
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

                uiInterface.SpaceshipShieldBarUi.Fill = _shield.FillShield;
                uiInterface.SpaceshipShieldTextUi.Text = _shield.PercentShield;

                if (_health.CurrentHealth < _health.AverageHealth)
                {
                    uiInterface.SpaceshipHealthBarUi.SetColor(Color.red);
                }
                // todo разобраться в случае если мы сможем подбирать аптечки и щиты,
                // как это будет отображаться
                if (_shield.CurrentShield <= 0)
                {
                    uiInterface.SpaceshipShieldBarUi.SetActive(false);
                    uiInterface.SpaceshipShieldTextUi.SetActive(false);

                    if (_health.CurrentHealth <= 0)
                    {
                        uiInterface.SpaceshipHealthBarUi.SetActive(false);
                        uiInterface.SpaceshipHealthTextUi.SetActive(false);
                    }
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