using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class PlayUI : MonoBehaviour
{
    public Button playBtn;
    public TMP_InputField seedInput;
    public string SceneToLoad;
    // Start is called before the first frame update
    void Awake()
    {
        playBtn = GetComponent<Button>();

        playBtn.onClick.AddListener(() =>
        {
            if (seedInput.text.Length > 0)
            {
                int.TryParse(seedInput.text, out int seed);
                Random.InitState(seed);
            }
            LevelManager.instance.LoadScene(SceneToLoad);
        });
    }
}
