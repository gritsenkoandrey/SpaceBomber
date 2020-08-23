using Assets.Scripts.Interface;
using Assets.Scripts.Model;
using UnityEngine;


namespace Assets.Scripts.Controller
{
    public sealed class EnemySpaceshipController : BaseController, IExecute, IInitialization
    {
        private SpawnEnemy _spawnEnemy;

        public void Initialization()
        {
            _spawnEnemy = Object.FindObjectOfType<SpawnEnemy>();
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            _spawnEnemy.SpawnAsteroid();
            _spawnEnemy.SpawnSpaceshipEnemies();
            EnemyManager.RollCall();
        }
    }
}