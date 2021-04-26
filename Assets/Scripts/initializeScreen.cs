using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeScreen : MonoBehaviour
{
    public Element element;
    public List<GameObject> levels;
    public List<Material> boxColors;
    public Element chosenElement;
    public List<GameObject> Lines;
    public Camera cam;
    public List<GameObject> lineLabels;
    public GameObject dropDown;
    public Element Hydrogen;
    public Element Oxygen;
    public string elementSelected = "";

    // Start is called before the first frame update
    void Start()
    {
        chooseElement(elementSelected);
        initScreen();
 
    }

    public void chooseElement(string elementSelect)
    {
        elementSelect = selectElement.elementNames;
        List<Element> elementList = new List<Element>{ Hydrogen, Oxygen };
        foreach (Element elementObject in elementList)
        {
            
            if (elementSelect == elementObject.elementName)
            {
                chosenElement = elementObject;
                
                
            }
        }
    }

    public void initScreen()
    {
        List<Material> colorsUsed = new List<Material>();
        Debug.Log(chosenElement);
        foreach (float i in chosenElement.linePositions)
        {
            Debug.Log(chosenElement.linePositions.IndexOf(i));
            levels[chosenElement.linePositions.IndexOf(i)].transform.position = new Vector3(3.41f, i - 2.91f, 0);
        }

        int sideSwitcher = 0;
        foreach (GameObject Line in Lines)
        {

            if (sideSwitcher % 2 == 0)
            {
                var pos = new Vector3(Line.transform.position.x - 3, Line.transform.position.y + 0, Line.transform.position.z);
                var newpos = cam.WorldToScreenPoint(pos);
                var index = Lines.IndexOf(Line);
                var text = lineLabels[index];
                text.transform.position = newpos;
            }
            if (sideSwitcher % 2 != 0)
            {
                var pos = new Vector3(Line.transform.position.x + 5, Line.transform.position.y + 0, Line.transform.position.z);
                var newpos = cam.WorldToScreenPoint(pos);
                var index = Lines.IndexOf(Line);
                var text = lineLabels[index];
                text.transform.position = newpos;
            }
            sideSwitcher += 1;

        }

    }

}
