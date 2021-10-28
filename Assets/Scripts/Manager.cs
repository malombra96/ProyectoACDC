using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Color color;
    public GameObject[] uis;
    void Start()
    {
        //if (uis == null)
            uis = GameObject.FindGameObjectsWithTag("ObjectImg");

            for (int i = 0; i < uis.Length; i++)
            {
                if (uis[i].GetComponent<Text>() != null)
                    uis[i].GetComponent<Text>().color = color;
                else
                    uis[i].GetComponent<Image>().color = color;
                
                if (uis[i].GetComponent<Shadow>() != null)
                    uis[i].GetComponent<Shadow>().effectColor = color;
            }
    }

    
}
    