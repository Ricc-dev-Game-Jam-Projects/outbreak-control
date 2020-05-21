using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *
 [23:43] Riccardoric: Bem vindo ao 
reverse the plague


Obrigado testar nosso jogo!

[23:44] Riccardoric: Nesse jogo é necessário ter cautela
você é o governador de uma grande região
que foi infectada por um vírus...

[23:44] Riccardoric: sua missão é fazer com que o vírus mate o
menos de pessoas possível até criar uma vacina,
é uma tarefa difícil, será que você é capaz?

[23:44] Riccardoric: barre fronteiras, coloque lockdown,
trave navios e aviões. faça tudo na
medida do necessário.

[23:44] Riccardoric: está pronto para essa responsabilidade?
*/




public class Information : MonoBehaviour
{
    public Queue<string> MyTexts;
    public TextMeshProUGUI MyTextBox;

    
    void Start()
    {
        MyTexts = new Queue<string>();
        MyTexts.Enqueue("Welcome to Reverse The Plague" +
            "" +
            "" +
            "  Thanks for testing our game.");
        MyTextBox.text = MyTexts.Dequeue();
        MyTexts.Enqueue("In this game," +
            " you are the Ruler of a enormous Region that is infected by a dangerous virus." +
            " You really need to be cautious...");
        MyTexts.Enqueue("Your goal is stop the virus that is infecting and killing all the citizens" +
            " of your region, until you don't find a cure.");
        MyTexts.Enqueue("Take actions, such as locking borders, establish a lockdown, close harbors and airports, " +
            "   but measure your costs," +
            "   it will affect your economies and population.");
        MyTexts.Enqueue("Can you take care of this?");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (MyTexts.Count != 0)
                MyTextBox.text = MyTexts.Dequeue();
            Debug.Log("press");
        }
    }
}
