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

    public bool StartAllClosed = true;

    void Start()
    {
        if(StartAllClosed) CloseAllTabs();

        foreach(MacroTab tab in tabs)
        {
            foreach(Button btn in tab.Activators)
            {
                btn.onClick.AddListener(() =>
                {
                    if (!tab.TheTab.activeInHierarchy)
                        CloseAllTabs();
                    tab.TheTab.gameObject.SetActive(!tab.TheTab.activeInHierarchy);
                });
            }
        }
    }

    public void CloseAllTabs()
    {
        foreach(MacroTab tab in tabs)
        {
            tab.TheTab.gameObject.SetActive(false);
        }
    }
}
