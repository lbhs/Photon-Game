using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initializeScreen : MonoBehaviour
{
    public Element element;
    public List<GameObject> levels;
    public List<Material> boxColors;
    public Element chosenElement;
    public List<GameObject> Lines;
    public Camera cam;
    public List<GameObject> lineLabels;
    public GameObject dropDown;
    public Element Hydrogen;
    
    // Start is called before the first frame update
    void Start()
    {
        Element Hydrogen = new Element("Hydrogen", new List<string> { "Violet", "Cyan", "Blue", "Red" }, new List<float> { 0, 6.15f, 7.2875f, 7.6875f, 7.875f, 7.96875f }, new List<int> { 0, 980, 1161, 1224, 1254, 1269 });
        chooseElement(Hydrogen);
        initScreen();
 
    }

    public void chooseElement(Element element)
    {
        chosenElement = element;
        Debug.Log(chosenElement.GetType());
    }

    public void initScreen()
    {
        Debug.Log(chosenElement.name);
        List<Material> colorsUsed = new List<Material>();

        foreach (float i in chosenElement.linePositions)
        {
            Debug.Log(chosenElement.linePositions.IndexOf(i));
            levels[chosenElement.linePositions.IndexOf(i)].transform.position = new Vector3(3.41f, i - 2.91f, 0);
        }

        int sideSwitcher = 0;
        foreach (GameObject Line in Lines)
        {

            if (sideSwitcher % 2 == 0)
            {
                var pos = new Vector3(Line.transform.position.x - 3, Line.transform.position.y + 0, Line.transform.position.z);
                var newpos = cam.WorldToScreenPoint(pos);
                var index = Lines.IndexOf(Line);
                var text = lineLabels[index];
                text.transform.position = newpos;
            }
            if (sideSwitcher % 2 != 0)
            {
                var pos = new Vector3(Line.transform.position.x + 5, Line.transform.position.y + 0, Line.transform.position.z);
                var newpos = cam.WorldToScreenPoint(pos);
                var index = Lines.IndexOf(Line);
                var text = lineLabels[index];
                text.transform.position = newpos;
            }
            sideSwitcher += 1;

        }

    }

}
