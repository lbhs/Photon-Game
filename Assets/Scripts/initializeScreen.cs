using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class initializeScreen : MonoBehaviour
{
    public Element element;
    public List<GameObject> levels;
    public List<GameObject> levels2;
    public List<Material> boxColors;
    public Element chosenElement;
    public Element chosenElement2;
    public List<GameObject> Lines;
    public List<GameObject> Lines2;
    public Camera cam;
    public List<GameObject> lineLabels;
    public GameObject dropDown;
    public Element Hydrogen;
    public Element Copper;
    public Element Oxygen;
    public string elementSelected = "Hydrogen";

    // Start is called before the first frame update
    void Start()
    {
        chooseElement(elementSelected);
        initScreen();
 
    }

    public void chooseElement(string elementSelect)
    {
        elementSelect = selectElement.elementNames;
        List<Element> elementList = new List<Element>{ Hydrogen, Copper, Oxygen };
        foreach (Element elementObject in elementList)
        {
            
            if (elementSelect == elementObject.elementName)
            {
                chosenElement = elementObject;
                if (chosenElement.elementName == "Oxygen")
                {
                    chosenElement2 = Copper;
                }
                
            }
        }
    }

    public void initScreen()
    {
        List<Material> colorsUsed = new List<Material>();
        Debug.Log(chosenElement);
        Debug.Log(chosenElement2);
 //       foreach (float i in chosenElement.linePositions)
  //      {
  //          Debug.Log(chosenElement.linePositions.IndexOf(i));
  //          levels[chosenElement.linePositions.IndexOf(i)].transform.position = new Vector3(0, i, 0);
 //       }

  //      foreach (float i in chosenElement2.linePositions)
  //      {
  //          Debug.Log(chosenElement2.linePositions.IndexOf(i));
  //          levels2[chosenElement2.linePositions.IndexOf(i)].transform.position = new Vector3(5.8f, i, 0);
  //      }

        foreach (GameObject level in levels)
        {
            var biggestkj = chosenElement.kJValues.Last();
            var position = chosenElement.kJValues[levels.IndexOf(level)] * 6.6 / biggestkj;
            level.transform.position = new Vector3(0, (float)position, 0);
        }

        foreach (GameObject level in levels2)
        {
            var biggestkj = chosenElement2.kJValues.Last();
            var position = chosenElement2.kJValues[levels2.IndexOf(level)] * 6.6 / biggestkj;
            level.transform.position = new Vector3(5.8f, (float)position, 0);
        }

        foreach (GameObject Line in Lines)
        {
            Line.transform.position = cam.WorldToScreenPoint(levels[Lines.IndexOf(Line)].transform.position + new Vector3(-1, 0, 0));
            var biggestkj = chosenElement.kJValues.Last();
            var newkj = biggestkj - chosenElement.kJValues[Lines.IndexOf(Line)];
            Line.GetComponent<Text>().text = "-" + newkj + " kJ";

        }

        foreach (GameObject Line in Lines2)
        {

            Line.transform.position = cam.WorldToScreenPoint(levels2[Lines2.IndexOf(Line)].transform.position + new Vector3(-1, 0, 0));
            var biggestkj = chosenElement2.kJValues.Last();
            var newkj = biggestkj - chosenElement2.kJValues[Lines2.IndexOf(Line)];
            Line.GetComponent<Text>().text = "-" + newkj + " kJ";
        }

    }

}
