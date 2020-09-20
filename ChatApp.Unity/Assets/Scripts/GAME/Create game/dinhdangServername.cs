using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class dinhdangServername : MonoBehaviour
{

    public Text Input;
    public Text message;
    public Button createGame;
    // Start is called before the first frame update
    void Start()
    {
        message.text = " ";
        createGame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        string input = Input.text;
        if (isServername(input))
        {
            message.text = "";
            createGame.enabled = true;
        }
        else
        {
            message.text = "error !";
            createGame.enabled = false;
        }
    }
    bool isServername(string input)
    {
        Regex regex1 = new Regex(@"^[a-zA-Z0-9_]+$");
        if (input.Length > 20)
        {
            return false;
        }

        if (regex1.IsMatch(input))
        {
            return true;
        }

        return false;
    }
}
