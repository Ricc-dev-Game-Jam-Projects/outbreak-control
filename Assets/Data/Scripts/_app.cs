using UnityEngine;
using UnityEngine.SceneManagement;

public class _app : MonoBehaviour
{
    public static _app instance;

    void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    private void Start()
    {
        SceneManager.LoadScene(1);
    }
}
