using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// Let me show you the basic things you can do to control the outbreak

[System.Serializable]
public class Section
{
    public GameObject Obj;
    public string Name;
}

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public Information information;

    public List<Section> Sections;
    //public GameObject TextSection;
    //public GameObject RegionSection;

    public Queue<UnityAction> Turing;

    Queue<string> Initial = new Queue<string>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        Turing = new Queue<UnityAction>();

        Initial.Enqueue("Welcome to Outbreak Control\n\n\n\nThanks for testing our game.");
        Initial.Enqueue("In this game," +
            " you are the Ruler of a enormous Region that is infected by a dangerous virus." +
            " You really need to be cautious...");
        Initial.Enqueue("Your goal is stop the virus that is infecting and killing all the citizens" +
            " of your region, until you don't find a cure.");
        Initial.Enqueue("Take actions, such as locking borders, establish a lockdown, close harbors and airports, " +
            "   but measure your costs," +
            "   it will affect your economies and population.");
        Initial.Enqueue("Can you take care of this?");
    }

    void Start()
    {
        Turing.Enqueue(() =>
        {
            CloseAll();
            Sections.Find(x => x.Name == "text").Obj.SetActive(true);
            information.SetNewTexts(Initial);
            //TextSection.SetActive(true);
        });
        Turing.Enqueue(() =>
        {
            CloseAll();
            Section section = Sections.Find(x => x.Name == "region");
            section.Obj.SetActive(true);
        });
        Turing.Enqueue(() =>
        {
            FindObjectOfType<LevelManager>().LoadScene("Game");
        });

        Turing.Dequeue()();
    }

    public void NextState()
    {
        if(Turing.Count != 0)
            Turing.Dequeue()();
    }

    public void CloseAll()
    {
        foreach(Section section in Sections)
        {
            section.Obj.SetActive(false);
        }
    }
}
