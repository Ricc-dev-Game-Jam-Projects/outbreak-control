using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnLoadScene : MonoBehaviour
{
    public string SceneName;
    public bool Quitt;

    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            LevelManager lvl = FindObjectOfType<LevelManager>();
            if(lvl == null)
            {
                lvl = gameObject.AddComponent<LevelManager>();
            }
            if (lvl)
            {
                if (Quitt)
                {
                    lvl.Quit();
                    return;
                }
                lvl.LoadScene(SceneName);
            }
        });
    }
}
