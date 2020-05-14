using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            if (hit = Physics2D.Raycast(ray.origin, ray.direction))
            {
                GameObject go = hit.collider.gameObject;
                if(go.GetComponent<ToggleGameObject>() != null)
                {
                    go.GetComponent<ToggleGameObject>().ToggleIt();
                } else if (go.GetComponent<RegionBehaviour>() != null)
                {
                    go.GetComponent<RegionBehaviour>().OnLMBUp();
                }
            }
        }
    }
}
