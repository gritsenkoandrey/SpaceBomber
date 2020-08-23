using Assets.Scripts.Interface;
using Assets.Scripts.ServiceLocators;


namespace Assets.Scripts.Controller
{
    public sealed class Controllers : IInitialization
    {
        private readonly IExecute[] _executeControllers;

        public int Length
        {
            get { return _executeControllers.Length; }
        }

        public IExecute this[int index]
        {
            get
            {
                return _executeControllers[index];
            }
        }

        public Controllers()
        {
            ServiceLocator.SetService(new InputController());
            ServiceLocator.SetService(new SpaceshipController());
            ServiceLocator.SetService(new EnemySpaceshipController());

            _executeControllers = new IExecute[3];
            _executeControllers[0] = ServiceLocator.Resolve<InputController>();
            _executeControllers[1] = ServiceLocator.Resolve<SpaceshipController>();
            _executeControllers[2] = ServiceLocator.Resolve<EnemySpaceshipController>();
        }

        public void Initialization()
        {
            foreach (var controller in _executeControllers)
            {
                if (controller is IInitialization initialization)
                {
                    initialization.Initialization();
                }
            }

            ServiceLocator.Resolve<InputController>().On();
            ServiceLocator.Resolve<SpaceshipController>().On();
            ServiceLocator.Resolve<EnemySpaceshipController>().On();
        }

        public void Cleanup()
        {
            ServiceLocator.Cleanup();
            ServiceLocatorMonoBehaviour.Cleanup();
        }
    }
}