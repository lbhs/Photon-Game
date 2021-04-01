using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines properties for elements
public class Element : MonoBehaviour
{
    // Defines Name of atom and the kJValues for the colorSet that the element emits
    public string name;
    public List<string> colors;
    public List<float> kJValues;
    public List<float> linePositions;

    
    // Defines the Element Object
    public Element(string Name, List<string> colorSet, List<float> lines, List<float> kJ)
    {
        name = Name;
        linePositions = lines;
        kJValues = kJ;
        colors = colorSet;
    }

}

