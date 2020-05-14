using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapUI : MonoBehaviour
{
    public Button PopulationButton;
    public TextMeshProUGUI PopulationSizeText;
    public TextMeshProUGUI InfectedText;

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
        if(RegionBehaviour.RegionSelected != null)
        {
            Region reg = RegionBehaviour.RegionSelected.Region;
            if(reg.Type != RegionType.Water && reg.city != null)
            {
                PopulationSizeText.text = reg.city.PopulationSize + "";
                InfectedText.text = reg.city.Infected + "";
            } 
        }
    }
}
