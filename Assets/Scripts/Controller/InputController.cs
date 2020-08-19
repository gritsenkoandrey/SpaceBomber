using Assets.Scripts.Interface;
using Assets.Scripts.ServiceLocators;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class InputController : BaseController, IExecute
    {
        private readonly int _leftButton = (int)MouseButton.LeftButton;
        private readonly int _rightButton = (int)MouseButton.RightButton;


        public InputController()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            if (Input.GetMouseButton(_leftButton))
            {
                if (ServiceLocator.Resolve<SpaceshipController>().IsActive)
                {
                    ServiceLocator.Resolve<SpaceshipController>().FireFirstWeapon();
                }
            }

            if (Input.GetMouseButton(_rightButton))
            {
                if (ServiceLocator.Resolve<SpaceshipController>().IsActive)
                {
                    ServiceLocator.Resolve<SpaceshipController>().FireSecondWeapon();
                }
            }
        }
    }
}