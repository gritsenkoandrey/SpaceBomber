using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.PoolObject
{
    [AddComponentMenu("Pool/ObjectPooling")]
    public sealed class ObjectPooling
    {
        private List<PoolObject> _objects;
        private Transform _objectsParent;

        /// <summary>
        /// Добавление происходит с помощью метода AddObject.
        /// Создается Gameobject temp, ему присваивается имя образца,
        /// после чего он добавляется в наш List. Затем объект выключается до тех пор,
        /// пока его не «потребуют» снаружи.
        /// </summary>
        /// <param name="sample">образец, который нужно добавить</param>
        /// <param name="objectsParent">родителm в иерархии на сцене</param>
        private void Addobject(PoolObject sample, Transform objectsParent)
        {
            var temp = GameObject.Instantiate(sample.gameObject);
            temp.name = sample.name;
            temp.transform.SetParent(objectsParent);
            _objects.Add(temp.GetComponent<PoolObject>());

            // если у объектов нет анимации, то можно данный код не указывать
            if (temp.GetComponent<Animator>())
            {
                temp.GetComponent<Animator>().StartPlayback();
            }

            temp.SetActive(false);
        }

        /// <summary>
        /// Инициализация Pool Object
        /// </summary>
        /// <param name="count"></param>
        /// <param name="sample"></param>
        /// <param name="objectsParent"></param>
        public void Initialize(int count, PoolObject sample, Transform objectsParent)
        {
            _objects = new List<PoolObject>(); //инициализируем List
            _objectsParent = objectsParent; //инициализируем локальную переменную для последующего использования
            for (int i = 0; i < count; i++)
            {
                Addobject(sample, objectsParent); //создаем объекты до указанного количества
            }
        }

        /// <summary>
        /// Проходимся по листу, если какой-то из объектов в пуле выключен
        /// (т.е. свободен) — возвращаем его, иначе добавляем новый
        /// </summary>
        /// <returns></returns>
        public PoolObject GetObject()
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].gameObject.activeInHierarchy == false)
                {
                    return _objects[i];
                }
            }
            Addobject(_objects[0], _objectsParent);
            return _objects[_objects.Count - 1];
        }
    }
}