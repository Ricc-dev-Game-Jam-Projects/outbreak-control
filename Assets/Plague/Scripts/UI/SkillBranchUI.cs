using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBranchUI : MonoBehaviour
{
    public TextMeshProUGUI[] SkillNames;
    public Image[] SkillImages;

    public void UpdateSkills(Perk[] perks)
    {
        for(int i = 0; i < perks.Length; i++)
        {
            SkillNames[i].text = perks[i].Name;
            SkillImages[i].sprite = Resources.Load<Sprite>(perks[i].ImagePath);
        }
    }
}
