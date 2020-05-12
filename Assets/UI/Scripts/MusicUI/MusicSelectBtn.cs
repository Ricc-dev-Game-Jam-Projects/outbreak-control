using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MusicSelectBtn : MonoBehaviour
{
    public int index;
    
    public void SetMusic(int _index, string name)
    {
        index = _index;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.instance.Play(index);
        });

        GetComponentInChildren<TextMeshProUGUI>().text = name;
    }
}
