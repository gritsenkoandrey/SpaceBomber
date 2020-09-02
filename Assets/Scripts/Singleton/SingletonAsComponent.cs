using UnityEngine;


namespace Assets.Scripts.Singleton
{
    public class SingletonAsComponent<T> : MonoBehaviour where T : SingletonAsComponent<T>
    {
        private static T _instance;

        protected static SingletonAsComponent<T> instance
        {
            get
            {
                if (!_instance)
                {
                    T[] managers = GameObject.FindObjectOfType(typeof(T)) as T[];

                    if (managers != null)
                    {
                        if (managers.Length == 1)
                        {
                            _instance = managers[0];
                            return _instance;
                        }
                        else if (managers.Length > 1)
                        {
                            Debug.LogError("You have more than one "
                                + typeof(T).Name + " in the scene. You only need 1, it's a singleton!");
                            for (int i = 0; i < managers.Length; i++)
                            {
                                T manager = managers[i];
                                Destroy(manager.gameObject);
                            }
                        }
                    }
                    GameObject go = new GameObject(typeof(T).Name, typeof(T));
                    _instance = go.GetComponent<T>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
            set
            {
                _instance = value as T;
            }
        }
    }
}