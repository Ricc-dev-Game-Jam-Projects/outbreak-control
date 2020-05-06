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

    public KeyCode MenuKey;

    private Image myBackground;

    private bool Closed = true;

    void Start()
    {
        myBackground = GetComponent<Image>();
        CloseAllTabs();
        if (myBackground != null)
        {
            myBackground.enabled = !Closed;
        }

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

    private void Update()
    {
        if(Input.GetKeyDown(MenuKey))
        {
            if (!Closed)
            {
                CloseAllTabs();
                
            } else if(tabs.Count >= 1)
            {
                tabs[0].TheTab.SetActive(true);
            }
            Closed = !Closed;
            if (myBackground != null)
            {
                myBackground.enabled = !Closed;
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
