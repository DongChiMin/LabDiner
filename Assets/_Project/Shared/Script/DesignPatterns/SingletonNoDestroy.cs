using UnityEngine;

public class SingletonNoDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this as T;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}
