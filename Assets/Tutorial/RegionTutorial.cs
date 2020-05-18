using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionTutorial : MonoBehaviour
{
    public Button SavePeople;

    void Awake()
    {
        SavePeople.onClick.AddListener(() =>
        {
            TutorialManager.instance.NextState();
        });
    }
}
