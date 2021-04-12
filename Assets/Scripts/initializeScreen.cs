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
        Hydrogen = new Element("Hydrogen",  new List<string> { "Violet", "Cyan", "Blue", "Red" }, new List<float> { 0, 6.15f, 7.2875f, 7.6875f, 7.875f, 7.96875f }, new List<int> { 0, 980, 1161, 1224, 1254, 1269 });
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

            if (Hydrogen.colors.Contains(i.name) == true) {

                colorsUsed.Add(i);

            }
            
        }

        foreach (float i in chosenElement.linePositions)
        {
            Debug.Log(chosenElement.linePositions.IndexOf(i));
            levels[chosenElement.linePositions.IndexOf(i)].transform.position = new Vector3(3.41f, i - 2.91f, 0);
        }
       
    }

}
