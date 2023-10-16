using UnityEngine;

namespace Util.Base
{
    public class BaseSingleton<T>: MonoBehaviour where T: BaseSingleton<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
            }
        }

        protected void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public static bool IsInit => _instance != null;
    }
}