using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeScreen : MonoBehaviour
{
    public Element Hydrogen;
    public List<GameObject> levels;
    public List<Material> boxColors;
    public Element chosenElement;

    // Start is called before the first frame update
    void Start()
    {
        Hydrogen = new Element("Hydrogen", new List<float> { 0, 6.15f, 7.2875f, 7.6875f, 7.875f, 7.96875f }, new List<String> { "Violet", "Cyan", "Red" }, new List<float> { 0, 6.15f, 7.2875f, 7.6875f, 7.875f, 7.96875f });
        chooseElement(Hydrogen);
        initScreen();
 
    }

    public void chooseElement(Element element)
    {
        chosenElement = element;
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

        foreach (float i in chosenElement.kJValues)
        {
            Debug.Log(chosenElement.kJValues.IndexOf(i));
            levels[chosenElement.kJValues.IndexOf(i)].transform.position = new Vector3(3.41f, i - 2.91f, 0);
        }
       
    }

}
