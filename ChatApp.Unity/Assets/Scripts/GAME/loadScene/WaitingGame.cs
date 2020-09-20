using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingGame : MonoBehaviour
{
    public void waitingGame()
    {
        SceneManager.LoadScene(6);
    }
}
