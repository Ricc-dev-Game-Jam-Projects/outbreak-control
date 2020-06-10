using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class VirusUI : MonoBehaviour
{
    public TextMeshProUGUI PlagueName;
    public TextMeshProUGUI PlagueDescription;
    public TextMeshProUGUI PlagueSkillName;
    public SkillBranchUI[] SkillBranchs;

    public Button ArrowLeft;
    public Button ArrowRight;

    public Virus MyVirus;
    public Perk[][] perks;

    public int Page = 0;

    private PerkGenerator perkGenerator;

    private void Start()
    {
        ArrowLeft.onClick.AddListener(() =>
        {
            if (MyVirus == null) return;
            Page--;
            if (Page < 0)
            {
                Page = 0;
            }
            UpdateTree();
        });

        ArrowRight.onClick.AddListener(() =>
        {
            if (MyVirus == null) return;
            Page++;
            if (Page >= MyVirus.PerkNumber)
            {
                Page = MyVirus.PerkNumber - 1;
            }
            UpdateTree();
        });
    }

    public void SetVirus(Virus virus, PerkGenerator perkGenerator)
    {
        this.perkGenerator = perkGenerator;
        MyVirus = virus;
        PlagueName.text = virus.Name;
        PlagueDescription.text = virus.ToString();

        UpdateTree();
    }

    public void UpdateTree()
    {
        if (MyVirus == null) return;
        string Skillkey = MyVirus.Perks.Keys.ElementAt(Page);
        PlagueSkillName.text = Skillkey;
        for(int i = 0; i < SkillBranchs.Length; i++)
        {
            switch(Page)
            {
                case 0:
                    if(MyVirus.MySymptoms[i] != null)
                        SkillBranchs[i].UpdateSkills(perkGenerator.SymptomsPerks[i], MyVirus.MySymptoms[i].PerkLevel);
                    else
                        SkillBranchs[i].UpdateSkills(perkGenerator.SymptomsPerks[i], 0);
                    break;
                case 1:
                    if (MyVirus.MyTransmissions[i] != null)
                        SkillBranchs[i].UpdateSkills(perkGenerator.TransmissionPerks[i], MyVirus.MyTransmissions[i].PerkLevel);
                    else
                        SkillBranchs[i].UpdateSkills(perkGenerator.TransmissionPerks[i], 0);
                    break;
            }
        }
    }
}
