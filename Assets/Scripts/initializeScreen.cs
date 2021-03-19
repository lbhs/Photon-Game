using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeScreen : MonoBehaviour
{
    public Element Hydrogen;
    public List<GameObject> colorBoxes;
    public List<Material> boxColors;

    // Start is called before the first frame update
    void Start()
    {
        Hydrogen = new Element("Hydrogen", new List<List<int>> { new List<int> { 158, 190 }, new List<int> { 201, 210 }, new List<int> { 264, 312 } }, new List<string> { "Violet", "Cyan", "Red" });
        initScreen();
    }

    public void initScreen()
    {
        
        foreach (Material i in boxColors)
        {
            if (Hydrogen.colorSet.Contains(i.name) == true) {
                Debug.Log(i.name);
            }
        }



    }
}
