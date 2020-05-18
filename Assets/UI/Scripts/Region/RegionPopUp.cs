using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class RegPopUp
{
    public string PopUpIdentifier;
    public Sprite PopUpSprite;
    public Vector2 PopUpPivot;
}

public class RegionPopUp : MonoBehaviour
{
    public RegPopUp[] RegPopUps;
    public SpriteRenderer PopUpSprite;

    public float lastX = -999;
    public float lastY = -999;

    private RegionBehaviour lastRegion;

    public void OpenOn(RegionBehaviour regionB)
    {
        float x = regionB.transform.position.x;
        float y = regionB.transform.position.y;

        if(TrumpBehaviour.Trump.IsBuildingWalls)
        {
            return;
        }

        if(lastRegion == regionB)
        {
            lastRegion = null;
            Close();
        } else
        {
            lastRegion = regionB;

            RegPopUp pop = null;
            float offset = .15f;

            if (x >= 0.0f && y >= 0.0f)
            {
                pop = RegPopUps.First(z => z.PopUpIdentifier == "UR");
                offset *= -1;
            } else if(x >= 0.0f && y < 0.0f)
            {
                pop = RegPopUps.First(z => z.PopUpIdentifier == "DR");
            } else if(x < 0.0f && y > 0.0f)
            {
                pop = RegPopUps.First(z => z.PopUpIdentifier == "UL");
                offset *= -1;
            } else
            {
                pop = RegPopUps.First(z => z.PopUpIdentifier == "DL");
            }
            if(pop != null)
            {
                PopUpSprite.sprite = pop.PopUpSprite;
                PopUpSprite.transform.GetChild(0).transform.localPosition = new Vector3(pop.PopUpPivot.x, pop.PopUpPivot.y, 30f);
            }

            PopUpSprite.transform.position = new Vector3(x, y + offset, 10f);
            PopUpSprite.gameObject.SetActive(true);
        }
    }

    public void Close()
    {
        PopUpSprite.gameObject.SetActive(false);
    }
}
