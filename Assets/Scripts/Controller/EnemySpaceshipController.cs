using Assets.Scripts.Interface;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class EnemySpaceshipController : BaseController, IExecute, IInitialization
    {
        private SpaceshipEnemy _spaceshipEnemy;

        public void Initialization()
        {
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

        }
    }
}