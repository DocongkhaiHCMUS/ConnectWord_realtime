using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dinhdang : MonoBehaviour
{
    public Text Input;
    public Text message;
    public Button creategame;
    // Start is called before the first frame update
    void Start()
    {
        message.text = " ";
        creategame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDigit(Input.text) )
        {
            message.text = "Only enter numbers ";
            creategame.enabled = false;
        }
        else
        {
            if(int.Parse(Input.text) < 2 || int.Parse(Input.text) > 200)
            {
                message.text = "1 < player < 200";
                creategame.enabled = false;
            }    
            else
            {
                message.text = " ";
                creategame.enabled = true;
            }    
        }
    }

    bool IsDigit(string input)
    {
        if (input.Length > 0)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                    return false;
            }
        }
        else
        {
            return false;
        }    
        
        return true;
    }
}
