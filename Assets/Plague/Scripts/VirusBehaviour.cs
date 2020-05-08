using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusBehaviour : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Virus virus;

    private PerkGenerator perkGenerator;

    void Start()
    {
        virus = new Virus("Hepy", 0.5f);
        Debug.Log("Virus " + virus.Name);
        text.text = virus.ToString();
        perkGenerator = new PerkGenerator();
        perkGenerator.GeneratePerks(out Symptom[] symptoms, out Transmission[] transmissions);
    }

    void Update()
    {
        
    }
}
