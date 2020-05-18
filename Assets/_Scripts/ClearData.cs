using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearData : MonoBehaviour
{
    public Button ClearDataBtn;

    private void Awake()
    {
        ClearDataBtn.onClick.AddListener(() =>
        {
            PlayerPrefs.DeleteAll();
            Application.Quit();
        });
    }
}
