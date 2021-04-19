using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    public GameObject dropDown;
    public string atomSelect;
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }

    
}
