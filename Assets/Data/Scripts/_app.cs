using UnityEngine;
using UnityEngine.SceneManagement;

public class _app : MonoBehaviour
{
    public static _app instance;

    public string TutorialScene;
    public string MenuScene;

    private string FirstPlayKey = "first_play";

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
        if (PlayerPrefs.GetInt(FirstPlayKey) != 0)
        {
            SceneManager.LoadScene(MenuScene);
        } else
        {
            PlayerPrefs.SetInt(FirstPlayKey, 1);
            SceneManager.LoadScene(TutorialScene);
        }
    }
}
