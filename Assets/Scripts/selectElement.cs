using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class selectElement : MonoBehaviour
{
    public static string elementNames;
    public Button button;

    public void SetElement()
    {
        button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        elementNames = button.name;
        Debug.Log(elementNames);
        SceneManager.LoadScene(1);
    }
}
