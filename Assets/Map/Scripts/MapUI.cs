using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class MapUI : MonoBehaviour
{
    public RegionPopUp PopUp;

    public Button PopulationButton;
    public TextMeshProUGUI TotalPopulationText;
    public TextMeshPro PopulationSizeText;
    public TextMeshPro InfectedText;
    public GameObject VirusBar;
    public GameObject PopBar;

    private bool showPopulation;

    void Start()
    {
        PopulationButton.onClick.AddListener(() =>
        {
            MapBehaviour.instance.ShowPopulation(showPopulation);
            showPopulation = !showPopulation;
        });
    }

    private void Update()
    {
        if (RegionBehaviour.RegionSelected != null)
        {
            Region reg = RegionBehaviour.RegionSelected.Region;
            if (reg != null && reg.Type != RegionType.Water)
            {
                PopulationSizeText.text = ((int)(reg.city.Population / City.Person)) + "";
                InfectedText.text = ((int)(reg.city.Infected / City.Person)) + "";
                VirusBar.transform.localScale =
                    new Vector3(reg.city.Infected / City.Person / 1000,
                    VirusBar.transform.localScale.y,
                    VirusBar.transform.localScale.z);
                PopBar.transform.localScale =
                    new Vector3(reg.city.Population / City.Person / 1000,
                    PopBar.transform.localScale.y,
                    PopBar.transform.localScale.z);
                PopUp.OpenOn(RegionBehaviour.RegionSelected.transform.position.x,
                    RegionBehaviour.RegionSelected.transform.position.y);
            }
            else
            {
                PopUp.Close();
            }
        }

        TotalPopulationText.text = ((int)(City.TotalPopulation / City.Person)).
            ToString("G", NumberFormatInfo.CurrentInfo);
    }
}
