using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public static string elementNames;

    public void startTutorial()
    {
        SceneManager.LoadScene(4);
        elementNames = "Hydrogen";
        Debug.Log(elementNames);
    }
}
