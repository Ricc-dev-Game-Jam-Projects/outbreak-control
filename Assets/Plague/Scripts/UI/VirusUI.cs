using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirusUI : MonoBehaviour
{
    public TextMeshProUGUI PlagueName;
    public TextMeshProUGUI PlagueDescription;
    public TextMeshProUGUI PlagueSkillName;
    public GameObject skillTree;

    public Button ArrowLeft;
    public Button ArrowRight;

    public Virus MyVirus;
    public Perk[][] perks;

    public int Page = 0;

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

    public void SetVirus(Virus virus)
    {
        MyVirus = virus;
        PlagueName.text = virus.Name;
        PlagueDescription.text = virus.ToString();
        perks = new Perk[virus.PerkNumber][];
        perks[0] = virus.MySymptoms.ToArray();
        perks[1] = virus.MyTransmissions.ToArray();


        UpdateTree();
    }

    public void UpdateTree()
    {
        if (MyVirus == null) return;
        // skill tree
        string Skillkey = MyVirus.Perks.Keys.ElementAt(Page);
        PlagueSkillName.text = Skillkey;
        SkillBranchUI[] skills = skillTree.GetComponentsInChildren<SkillBranchUI>();
        for(int i = 0; i < skills.Length; i++)
        {
            skills[i].UpdateSkills(MyVirus.Perks[Skillkey]);
        }
    }
}
