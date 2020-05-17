using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit;

        if (hit = Physics2D.Raycast(ray.origin, ray.direction))
        {
            GameObject go = hit.collider.gameObject;
            RegionBehaviour regb = go.GetComponent<RegionBehaviour>();
            if (Input.GetMouseButtonUp(0))
            {
                if (go.GetComponent<ToggleGameObject>() != null)
                {
                    go.GetComponent<ToggleGameObject>().ToggleIt();
                }
                else if (regb != null && regb.enabled)
                {
                    regb.OnLMBUp();
                }
            }
            else if(Input.GetMouseButtonUp(1))
            {
                if (regb != null)
                {
                    regb.OnRMBUp();
                }
            }
        }
         
    }
}
