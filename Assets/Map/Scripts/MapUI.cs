using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    public RegionPopUp PopUp;

    public Button PopulationButton;
    public TextMeshPro PopulationSizeText;
    public TextMeshPro InfectedText;
    public GameObject VirusBar;

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
                PopulationSizeText.text = ((int)(reg.city.Population * 1e3)) + "";
                //InfectedText.text = ((int)(reg.city.Infected * 1e3)) + "";
                VirusBar.transform.localScale =
                    new Vector3(reg.city.Infected / reg.city.Population,
                    VirusBar.transform.localScale.y,
                    VirusBar.transform.localScale.z);
                PopUp.OpenOn(RegionBehaviour.RegionSelected.transform.position.x,
                    RegionBehaviour.RegionSelected.transform.position.y);
            }
            else
            {
                PopUp.Close();
            }
        }
    }
}
