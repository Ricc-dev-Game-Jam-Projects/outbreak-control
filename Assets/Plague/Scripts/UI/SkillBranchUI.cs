using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillBranchUI : MonoBehaviour
{
    public TextMeshProUGUI SkillName;
    public Image[] ImagePlace;
    public Image[] SkillImages;
    public Sprite Active;
    public Sprite Deactive;

    public void UpdateSkills(Perk[] perks, int activePerks)
    {
        if(activePerks != 0)
            SkillName.text = perks[activePerks - 1].Name;
        for(int i = 0; i < perks.Length; i++)
        {
            SkillImages[i].sprite = Resources.Load<Sprite>(perks[i].ImagePath);
        }
        for(int i = 1; i <= ImagePlace.Length; i++)
        {
            if (activePerks < i)
            {
                ImagePlace[i - 1].sprite = Deactive;
            } else
            {
                ImagePlace[i - 1].sprite = Active;
            }
        }
    }
}
