using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class SpawnController : BaseController, IExecute, IInitialization
    {
        private Spawn _spawn;

        public void Initialization()
        {
            _spawn = Object.FindObjectOfType<Spawn>();
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            _spawn.SpawnAsteroid();
            _spawn.SpawnSpaceshipEnemies();
            EnemyManager.RollCall();
        }
    }
}