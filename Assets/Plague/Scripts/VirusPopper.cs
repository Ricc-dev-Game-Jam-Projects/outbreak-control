using UnityEngine;

[RequireComponent(typeof(VirusPopper))]
public class VirusPopper : MonoBehaviour
{
    public bool Able = false;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Pop(RegionBehaviour region)
    {
        gameObject.transform.SetParent(region.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        Able = false;
        animator.Play("PlagueCase");
        //Debug.Log("Popped on x: " + region.Region.X + "  y: " + region.Region.Y);
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("idle"))
        {
            Able = true;
        } else
        {
            Able = false;
        }
    }
}
