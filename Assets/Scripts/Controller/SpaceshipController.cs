using Assets.Scripts.Interface;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class SpaceshipController : BaseController, IExecute, IInitialization
    {
        private IMove _move;
        private SpaceshipFire _fire;

        public void Initialization()
        {
            _fire = Object.FindObjectOfType<SpaceshipFire>();
            _move = Object.FindObjectOfType<SpaceshipMove>();
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