﻿using System.Collections;
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
    public List<GameObject> NLines;
    public List<GameObject> NLines2;
    public Camera cam;
    public GameObject dropDown;
    public Text wellTitle1;
    public Text wellTitle2;
    public Element Hydrogen;
    public Element X;
    public Element Y;
    public Element A;
    public Element B;
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
        List<Element> elementList = new List<Element>{ Hydrogen, X, A };
        foreach (Element elementObject in elementList)
        {

            if (elementSelect == elementObject.elementName)
            {
                chosenElement = elementObject;
                if (chosenElement.elementName == "X")
                {
                    chosenElement2 = Y;
                }
                if (chosenElement.elementName == "A")
                {
                    chosenElement2 = B;
                }

            }


        }
    }

    public void initScreen()
    {
        List<Material> colorsUsed = new List<Material>();
        wellTitle1.text = "Element " + chosenElement.elementName;
        wellTitle2.text = "Element " + chosenElement2.elementName;
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
            var position = chosenElement.kJValues[levels.IndexOf(level)] * (levels[4].transform.position.y - levels[0].transform.position.y) / biggestkj;
            level.transform.position = new Vector3(levels[0].transform.position.x, levels[0].transform.position.y + (float)position, 0);
        }

        foreach (GameObject level in levels2)
        {
            var biggestkj = chosenElement2.kJValues.Last();
            var position = chosenElement2.kJValues[levels2.IndexOf(level)] * (levels2[4].transform.position.y - levels2[0].transform.position.y) / biggestkj;
            level.transform.position = new Vector3(levels2[0].transform.position.x, levels2[0].transform.position.y + (float)position, 0);
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

        foreach (GameObject Line in NLines)
        {

            Line.transform.position = cam.WorldToScreenPoint(levels[NLines.IndexOf(Line)].transform.position + new Vector3(3.8f, 0, 0));

        }

        foreach (GameObject Line in NLines2)
        {

            Line.transform.position = cam.WorldToScreenPoint(levels2[NLines2.IndexOf(Line)].transform.position + new Vector3(3.8f, 0, 0));
        }

    }

}
