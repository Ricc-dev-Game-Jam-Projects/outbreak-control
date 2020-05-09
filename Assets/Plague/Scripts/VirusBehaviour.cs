using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class VirusBehaviour : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Virus virus;
    public VirusUI _VirusUI;

    private PerkGenerator perkGenerator;

    void Start()
    {
        virus = new Virus("Hepy", 0.5f);
        Debug.Log("Virus " + virus.Name);
        text.text = virus.ToString();
        perkGenerator = new PerkGenerator();
        perkGenerator.GeneratePerks(out Symptom[] symptoms, out Transmission[] transmissions);

        virus.MySymptoms.AddRange(symptoms);
        virus.MyTransmissions.AddRange(transmissions);

        if(_VirusUI != null) _VirusUI.SetVirus(virus);
    }
}
