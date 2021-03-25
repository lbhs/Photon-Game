using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeScreen : MonoBehaviour
{
    public Element Hydrogen;
    public List<GameObject> levels;
    public List<Material> boxColors;

    // Start is called before the first frame update
    void Start()
    {
        Hydrogen = new Element("Hydrogen", new List<int> { 0, 984, 1166, 1230, 1260, 1275 }, new List<string> { "Violet", "Cyan", "Red" });
        initScreen();
    }

    public void initScreen()
    {
        List<Material> colorsUsed = new List<Material>();

        foreach (Material i in boxColors)
        {

            if (Hydrogen.colorSet.Contains(i.name) == true) {

                colorsUsed.Add(i);

            }
            
        }
       
    }

}
