using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class VirusBehaviour : MonoBehaviour
{
    public static VirusBehaviour instance;

    public TextMeshProUGUI text;
    public Virus virus;
    public VirusUI _VirusUI;

    public GameObject virusPopperPrefab;
    public List<GameObject> virusPopperPool;

    public RegionBehaviour regionBehaviourExample;

    private PerkGenerator perkGenerator;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        virusPopperPool = new List<GameObject>();
        virus = new Virus("Hepy", 0.5f);
        perkGenerator = new PerkGenerator();
        perkGenerator.GeneratePerks(out Symptom[] symptoms, out Transmission[] transmissions);
        Virus.CalculateSpreading(virus);

        virus.MySymptoms.AddRange(symptoms);
        virus.MyTransmissions.AddRange(transmissions);

        if (text != null) text.text = virus.ToString();
        if (_VirusUI != null) _VirusUI.SetVirus(virus, perkGenerator);

        foreach(RegionBehaviour regionBehaviour in RegionBehaviour.Regions)
        {
            regionBehaviour.SubscribeOnInfected(() =>
            {
                GetPopper().GetComponent<VirusPopper>().Pop(regionBehaviour);
            });
        }
    }

    //eh assim que funcionam as balas de um revolver
    public GameObject GetPopper()
    {
        GameObject popper;
        foreach (GameObject pop in virusPopperPool)
        {
            if (pop.GetComponent<VirusPopper>().Able())
            {
                return pop;
            }
        }
        popper = Instantiate(virusPopperPrefab);
        virusPopperPool.Add(popper);
        return popper;
    }
}
