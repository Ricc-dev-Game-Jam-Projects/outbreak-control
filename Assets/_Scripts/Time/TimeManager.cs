using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Timer timer;

    public TextMeshProUGUI TimeText;

    void Update()
    {
        if(TimeText != null)
        {
            TimeText.text = timer.Day + "";
        }
    }
}
