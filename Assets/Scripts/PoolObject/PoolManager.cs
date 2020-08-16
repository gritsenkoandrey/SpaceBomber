using System;
using UnityEngine;


namespace Assets.Scripts.PoolObject
{
    /// <summary>
    /// Следующий класс PoolManager управляет пулами различных объектов.
    /// Класс статический для упрощения доступа к объектам,
    /// т.е. не нужно создавать синглтоны, инстансы и прочее.
    /// </summary>
    public static class PoolManager
    {
        private static PoolPart[] _pools;
        private static GameObject _objectsParent;

        [Serializable]
        public struct PoolPart
        {
            public string Name; //имя префаба
            public PoolObject Prefab; //сам префаб, как образец
            public int Count; //количество объектов при инициализации пула
            public ObjectPooling Ferula; //сам пул
        }

        /// <summary>
        /// Инициализация производится массивом структур
        /// </summary>
        /// <param name="newPool">массив структуры</param>
        public static void Initialize(PoolPart[] newPool)
        {
            _pools = newPool; //заполняем информацию
            _objectsParent = new GameObject(); //создаем на сцене объект Pool, чтобы не заслонять иерархию
            _objectsParent.name = "Pool";

            for (int i = 0; i < _pools.Length; i++)
            {
                if (_pools[i].Prefab != null)
                {
                    //создаем свой пул для каждого префаба
                    _pools[i].Ferula = new ObjectPooling();
                    //инициализируем пул заданным количество объектов
                    _pools[i].Ferula.Initialize(_pools[i].Count, _pools[i].Prefab, _objectsParent.transform);
                }
            }
        }

        /// <summary>
        /// аналог стандартного Instantiate, но по имени объекта.
        /// Он проверяет все существующие пулы, и если находит
        /// правильный — дергает его метод GetObject() у класса ObjectPooling
        /// </summary>
        public static GameObject GetObject(string name, Vector3 position, Quaternion rotation)
        {
            GameObject result = null;
            if (_pools != null)
            {
                for (int i = 0; i < _pools.Length; i++)
                {
                    if (string.Compare(_pools[i].Name, name) == 0) //если имя совпало с именем префаба пула
                    {
                        result = _pools[i].Ferula.GetObject().gameObject; //дергаем объект из пула
                        result.transform.position = position;
                        result.transform.rotation = rotation;
                        result.SetActive(true); //выставляем координаты и активируем
                        return result;
                    }
                }
            }
            return result; //если такого объекта нет в пулах, вернет null
        }
    }
}