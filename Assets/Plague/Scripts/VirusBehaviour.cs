using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusBehaviour : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Virus virus;
    void Start()
    {
        virus = new Virus("Hepy", ETransmission.Airborne | ETransmission.Bloodborne, 0.5f);
        text.text = virus.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
