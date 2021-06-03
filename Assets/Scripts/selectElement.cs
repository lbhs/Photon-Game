using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class selectElement : MonoBehaviour
{
    // This variable holds the selected element (this is my way of transferring the data from the select screen to the actual game)
    public static string elementNames;
    public Button button;

    // This runs when someone selects an element pair in the menu
    public void SetElement()
    {
        // IMPORTANT: When the user clicks a button, the title of that button is saved as the elements name, make sure when you add new element pairs to name the title of the button
        button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        elementNames = button.name;
        Debug.Log(elementNames);
        SceneManager.LoadScene(2);
    }

    // This runs only when the tutorial button is clicked
    public void startTutorial()
    {
        SceneManager.LoadScene(4);
        elementNames = "Hydrogen";
        Debug.Log(elementNames);
    }

}
