using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public void Toggle(GameObject ToggleObj)
    {
        ToggleObj.SetActive(!ToggleObj.activeInHierarchy);
    }
}
