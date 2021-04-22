using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines properties for elements
public class Element : MonoBehaviour
{
    // Defines Name of atom and the kJValues for the colorSet that the element emits
    public string name;
    public List<string> colors;
    public List<float> linePositions;
    public List<int> kJValues;
  
    


    // Defines the Element Object
    public Element(string Name, List<string> colorSet, List<float> lines, List<int> kJ)
    {
        name = Name;
        linePositions = lines;
        kJValues = kJ;
        colors = colorSet;

    }



}

