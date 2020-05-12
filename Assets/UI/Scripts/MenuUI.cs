using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public TabUI MenuTab;
    public KeyCode MenuKey;

    public bool Closed = true;

    private void Start()
    {
        if (MenuTab.gameObject != null)
        {
            MenuTab.gameObject.SetActive(!Closed);
        } else
        {
            Debug.LogError("<color=purple>MenuUI >> What man??? Missing MenuTab reference...</color>");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(MenuKey))
        {
            if (!Closed)
            {
                MenuTab.CloseAllTabs();
                MenuTab.gameObject.SetActive(false);
            }
            else if (MenuTab.tabs.Count >= 1)
            {
                MenuTab.gameObject.SetActive(true);
                MenuTab.tabs[0].TheTab.SetActive(true);
            }
            Closed = !Closed;
        }
    }
}
