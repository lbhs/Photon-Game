using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines properties for elements
public class Element : MonoBehaviour
{
    // Defines Name of atom and the kJValues for the colorSet that the element emits
    public string Name;
    public List<float> kJValues;
    public List<string> colors;
    public List<float> linePositions;

    
    // Defines the Element Object
    public Element(string name, List<float> lines, List<string> colorSet, List<float> kJ)
    {
        Name = name;
        linePositions = lines;
        kJValues = kJ;
        colors = colorSet;
    }

}

