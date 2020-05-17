using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarIconsMove : MonoBehaviour
{
    public Transform VirusBar;
    public Transform VirusIcon;
    public Transform PopBar;
    public Transform PopIcon;

    void Update()
    {
        VirusIcon.localPosition = new Vector3(
            1f + (1.22f * VirusBar.localScale.x),
            VirusIcon.localPosition.y,
            VirusIcon.localPosition.z);
        PopIcon.localPosition = new Vector3(
            1f + (1.22f * PopBar.localScale.x),
            PopIcon.localPosition.y,
            PopIcon.localPosition.z);
    }
}
