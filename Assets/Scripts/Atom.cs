using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines properties for elements
public class Element : MonoBehaviour
{
    // Defines Name of atom and the kJValues for the colorSet that the element emits
    public string Name;
    public List<int> kJValues;
    public List<string> colorSet;
    
    // Defines the Element Object
    public Element(string name, List<int> kJ, List<string> colors)
    {
        Name = name;
        kJValues = kJ;
        colorSet = colors;
    }

}

