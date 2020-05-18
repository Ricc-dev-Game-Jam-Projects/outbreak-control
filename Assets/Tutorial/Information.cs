using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Information : MonoBehaviour
{
    public Animator animator;

    public Queue<string> MyTexts;
    public TextMeshProUGUI MyTextBox;

    public void SetNewTexts(Queue<string> myTexts)
    {
        MyTexts = myTexts;
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0) && animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            ShowNextText();
            Debug.Log("press");
        }
    }

    void ShowNextText()
    {
        if (MyTexts.Count != 0)
        {
            MyTextBox.text = MyTexts.Dequeue();
            animator.SetTrigger("OnClick");
        } else
        {
            TutorialManager.instance.NextState();
        }
    }
}
