using UnityEngine;

[RequireComponent(typeof(VirusPopper))]
public class VirusPopper : MonoBehaviour
{
    public bool Able = false;
    private Animator animator;
    private float TimeAt = 0f;
    private float TimeTo = 10f;

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
        //animator.Play("PlagueCase");
        animator.SetBool("Checked", false);
        animator.SetTrigger("Case");
        TimeAt = 0f;
        //Debug.Log("Popped on x: " + region.Region.X + "  y: " + region.Region.Y);
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("idle"))
        {
            Able = true;
            gameObject.transform.parent = null;
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localScale = Vector3.one;
        } else
        {
            Able = false;
        }

        if (!Able)
        {
            TimeAt += Time.deltaTime;
            if(TimeAt >= TimeTo)
            {
                TimeAt = 0f;
                animator.SetBool("Checked", true);
            }
        }
    }

    private void OnMouseEnter()
    {
        animator.SetBool("Checked", true);
    }
}
