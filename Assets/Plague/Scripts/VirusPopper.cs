using UnityEngine;

[RequireComponent(typeof(VirusPopper))]
public class VirusPopper : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Pop(RegionBehaviour region)
    {
        if (region.Region.Type == RegionType.Water) return;
        gameObject.transform.SetParent(region.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        animator.Play("PlagueCase");
        Debug.Log("Popped on x: " + region.Region.X + "  y: " + region.Region.Y);
    }

    public bool Able()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("PlagueCase");
    }
}
