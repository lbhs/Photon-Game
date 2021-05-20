using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class that defines properties for elements
public class Element : MonoBehaviour
{
    // Defines Name of atom and the kJValues for the colorSet that the element emits
    public string elementName;
    public List<int> kJValues;
  
    


    // Defines the Element Object
    public Element(string Name, List<int> kJ)
    {
        elementName = Name;
        kJValues = kJ;

    }



}

