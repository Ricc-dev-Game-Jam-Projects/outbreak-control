using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class MapUI : MonoBehaviour, IBehaviour
{
    public RegionPopUp PopUp;

    public Button PopulationButton;
    public TextMeshProUGUI TotalPopulationText;
    public TextMeshPro PopulationSizeText;
    public TextMeshPro InfectedText;
    public GameObject VirusBar;
    public GameObject PopBar;
    public GameObject ImmuneBar;

    private bool showPopulation;

    public void MyAwake()
    {
        // ...
    }

    public void MyStart()
    {
        PopulationButton.onClick.AddListener(() =>
        {
            MapBehaviour.instance.ShowPopulation(showPopulation);
            showPopulation = !showPopulation;
        });

        RegionBehaviour.SubscribeOnClickLMB((region) =>
        {
            if (!region.Region.IsWater()) PopUp.OpenOn(region);
        });
    }

    private void Update()
    {
        if (RegionBehaviour.RegionSelected != null)
        {
            Region reg = RegionBehaviour.RegionSelected.Region;
            if (reg != null && !reg.IsWater())
            {
                VirusBar.transform.localScale =
                    new Vector3(reg.city.population.SymptomaticDensity,
                    VirusBar.transform.localScale.y,
                    VirusBar.transform.localScale.z);
                PopBar.transform.localScale =
                    new Vector3(reg.city.population.Density,
                    PopBar.transform.localScale.y,
                    PopBar.transform.localScale.z);
                ImmuneBar.transform.localScale =
                    new Vector3(reg.city.population.Immune / reg.city.population.Total,
                    ImmuneBar.transform.localScale.y,
                    ImmuneBar.transform.localScale.z);
            }
            else
            {
                PopUp.Close();
            }
        }

        TotalPopulationText.text = ((int)(City.TotalPopulation / Population.p)).
            ToString("G", NumberFormatInfo.CurrentInfo);
    }
}
