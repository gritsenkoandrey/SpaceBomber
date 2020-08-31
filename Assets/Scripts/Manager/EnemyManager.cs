using System.Collections.Generic;


namespace Assets.Scripts.Model
{
    public static class EnemyManager
    {
        private static List<SpaceshipEnemy> _enemiesList;

        static EnemyManager()
        {
            _enemiesList = new List<SpaceshipEnemy>();
        }

        public static void AddEnemieToList(SpaceshipEnemy enemy)
        {
            if (!_enemiesList.Contains(enemy))
            {
                _enemiesList.Add(enemy);
                enemy.OnDieChange += RemoveEnemieToList;
            }
        }

        public static void RemoveEnemieToList(SpaceshipEnemy enemy)
        {
            if (!_enemiesList.Contains(enemy))
            {
                return;
            }
            enemy.OnDieChange -= RemoveEnemieToList;
            _enemiesList.Remove(enemy);
        }

        /// <summary>
        /// Вызов у каждого активного корабля на сцене метод Execute
        /// </summary>
        public static void RollCall()
        {
            for (int i = 0; i < _enemiesList.Count; i++)
            {
                _enemiesList[i].Execute();
            }
        }

        public static void Cleanup()
        {
            _enemiesList.Clear();
        }
    }
}