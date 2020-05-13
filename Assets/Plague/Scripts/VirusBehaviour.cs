using UnityEngine;
using TMPro;

public class VirusBehaviour : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Virus virus;
    public VirusUI _VirusUI;

    private PerkGenerator perkGenerator;
    float Spreading 

    void Start()
    {
        virus = new Virus("Hepy", 0.5f,0.3f);
        perkGenerator = new PerkGenerator();
        perkGenerator.GeneratePerks(out Symptom[] symptoms, out Transmission[] transmissions);
        Virus.CalculateSpreading(virus);

        virus.MySymptoms.AddRange(symptoms);
        virus.MyTransmissions.AddRange(transmissions);

        if(text != null) text.text = virus.ToString();
        if(_VirusUI != null) _VirusUI.SetVirus(virus, perkGenerator);
    }
}
