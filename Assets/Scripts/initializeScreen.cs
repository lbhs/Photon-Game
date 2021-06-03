using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class initializeScreen : MonoBehaviour
{
    // These represent the actual lines gameobjects that the user clicks on
    public List<GameObject> levels;
    public List<GameObject> levels2;

    // These represent the two elements that are chosen in the selection menu
    public Element chosenElement;
    public Element chosenElement2;

    // These represent the kJ value text next to the lines
    public List<GameObject> Lines;
    public List<GameObject> Lines2;

    // These represent the n value text next to the lines
    public List<GameObject> NLines;
    public List<GameObject> NLines2;


    // These represent the well titles below the two wells
    public Text wellTitle1;
    public Text wellTitle2;

    // These represent the different elements that exist (if you want to add new elements you have to add them here aswell)
    public Element Hydrogen;
    public Element X;
    public Element Y;
    public Element A;
    public Element B;
    public Element C;
    public Element K;

    // This represents the element that is selected from the selection screen
    public string elementSelected;

    // Camera
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        chooseElement(elementSelected);
        initScreen();
 
    }

    public void chooseElement(string elementSelect)
    {
        // selectElement is a script which is a property elementNames, elementNames is the name (as a string) of the element that the player chose in the selection menu
        elementSelect = selectElement.elementNames;

        // Only listed the first element of each pair (makes sense if you look at the comments below)
        List<Element> elementList = new List<Element>{ Hydrogen, X, A, C };
        foreach (Element elementObject in elementList)
        {
            // Checks through to see if the items in elementList match with elementSelect (elementSelect is the "elementNames" property from the selectElement script). If it does match with one of them, chosenElement is set to it and chosenElement2 is set to its pair
            if (elementSelect == elementObject.elementName)
            {
                chosenElement = elementObject;
                if (chosenElement.elementName == "Hydrogen")
                {
                    // This if statement is only true if the user goes through the tutorial, in the tutorial Hydrogen doesn't have a pair.
                    return;
                }
                if (chosenElement.elementName == "X")
                {
                    chosenElement2 = Y;
                }
                if (chosenElement.elementName == "A")
                {
                    chosenElement2 = B;
                }
                if (chosenElement.elementName == "C")
                {
                    chosenElement2 = K;
                }

            }


        }
    }

    public void initScreen()
    {
        // This code is very rigid because it deals with the tutorial where we want to manually change certain variables, don't worry, for the real game the code is much easier to alter
        if (chosenElement.elementName == "Hydrogen")
        {
            // Sets well title text
            wellTitle1.text = "Hydrogen";

            // The lines are manually moved so that the kJ and N value text don't overlap each other, therefore the spacing isn't exactly representative of the kJ values of the levels but this can be changed
            levels[4].transform.position = new Vector3(0.1899996f, 6.12f, 0);
            levels[3].transform.position = new Vector3(0.1899996f, 5.7f, 0);
            levels[2].transform.position = new Vector3(0.1899996f, 5f, 0);
            levels[1].transform.position = new Vector3(0.1899996f, 4f, 0);
            levels[0].transform.position = new Vector3(0.1899996f, -.48f, 0);

            // Iterates through the kJvalue text list to pair them up with each of the actual lines (You can, probably should, rename these lists to something more descriptive, we've just gotten used to them by now)
            foreach (GameObject Line in Lines)
            {
                Line.transform.position = cam.WorldToScreenPoint(levels[Lines.IndexOf(Line)].transform.position + new Vector3(-1, 0, 0));
                var biggestkj = chosenElement.kJValues.Last();
                var newkj = biggestkj - chosenElement.kJValues[Lines.IndexOf(Line)];
                Line.GetComponent<Text>().text = "-" + newkj + " kJ";

            }

            // Iterates through the n value text list to pair them up with each of the actual lines
            foreach (GameObject Line in NLines)
            {

                Line.transform.position = cam.WorldToScreenPoint(levels[NLines.IndexOf(Line)].transform.position + new Vector3(3.8f, 0, 0));

            }

            return;
        }

        // Tutorial specific code ends


        // This code is used when the player goes in the actual game

        // Sets the titles of the wells
        wellTitle1.text = "Element " + chosenElement.elementName;
        wellTitle2.text = "Element " + chosenElement2.elementName;

        // Sets the positions of the actual lines in the first well
        foreach (GameObject level in levels)
        {
            var biggestkj = chosenElement.kJValues.Last();
            var position = chosenElement.kJValues[levels.IndexOf(level)] * (levels[4].transform.position.y - levels[0].transform.position.y) / biggestkj;
            level.transform.position = new Vector3(levels[0].transform.position.x, levels[0].transform.position.y + (float)position, 0);
        }

        // Sets the positions of the actual lines in the second well
        foreach (GameObject level in levels2)
        {
            var biggestkj = chosenElement2.kJValues.Last();
            var position = chosenElement2.kJValues[levels2.IndexOf(level)] * (levels2[4].transform.position.y - levels2[0].transform.position.y) / biggestkj;
            level.transform.position = new Vector3(levels2[0].transform.position.x, levels2[0].transform.position.y + (float)position, 0);
        }
        // Iterates through the kJ value text list to pair them up with each of the actual lines in well 1
        foreach (GameObject Line in Lines)
        {
            Line.transform.position = cam.WorldToScreenPoint(levels[Lines.IndexOf(Line)].transform.position + new Vector3(-1, 0, 0));
            var biggestkj = chosenElement.kJValues.Last();
            var newkj = biggestkj - chosenElement.kJValues[Lines.IndexOf(Line)];
            Line.GetComponent<Text>().text = "-" + newkj + " kJ";

        }

        // Iterates through the kJ value text list to pair them up with each of the actual lines in well 2
        foreach (GameObject Line in Lines2)
        {

            Line.transform.position = cam.WorldToScreenPoint(levels2[Lines2.IndexOf(Line)].transform.position + new Vector3(-1, 0, 0));
            var biggestkj = chosenElement2.kJValues.Last();
            var newkj = biggestkj - chosenElement2.kJValues[Lines2.IndexOf(Line)];
            Line.GetComponent<Text>().text = "-" + newkj + " kJ";
        }

        // Iterates through the n value text list to pair them up with each of the actual lines in well 1
        foreach (GameObject Line in NLines)
        {

            Line.transform.position = cam.WorldToScreenPoint(levels[NLines.IndexOf(Line)].transform.position + new Vector3(3.8f, 0, 0));

        }

        // Iterates through the n value text list to pair them up with each of the actual lines in well 2
        foreach (GameObject Line in NLines2)
        {

            Line.transform.position = cam.WorldToScreenPoint(levels2[NLines2.IndexOf(Line)].transform.position + new Vector3(3.8f, 0, 0));
        }

    }

}
