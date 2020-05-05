using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MacroTab
{
    public List<Button> Activators;
    public GameObject TheTab;
}

public class TabUI : MonoBehaviour
{
    [SerializeField]
    public List<MacroTab> tabs;
    
    void Start()
    {
        foreach(MacroTab tab in tabs)
        {
            foreach(Button btn in tab.Activators)
            {
                btn.onClick.AddListener(() =>
                {
                    CloseAllTabs();
                    tab.TheTab.gameObject.SetActive(true);
                });
            }
        }
    }
    
    void CloseAllTabs()
    {
        foreach(MacroTab tab in tabs)
        {
            tab.TheTab.gameObject.SetActive(false);
        }
    }
}
