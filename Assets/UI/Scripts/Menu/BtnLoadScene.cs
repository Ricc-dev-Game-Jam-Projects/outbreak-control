using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BtnLoadScene : MonoBehaviour
{
    public string SceneName;

    private Button btn;

    void Awake()
    {
        btn = GetComponent<Button>();

        btn.onClick.AddListener(() =>
        {
            LevelManager lvl = FindObjectOfType<LevelManager>();
            if (lvl)
            {
                lvl.LoadScene(SceneName);
            }
        });
    }
}
