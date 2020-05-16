using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject ToggleObject;

    public void ToggleIt()
    {
        ToggleObject.SetActive(false);
    }
}
