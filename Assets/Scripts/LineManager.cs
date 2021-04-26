using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public List<GameObject> Lines;
    public GameObject electron;
    public List<GameObject> lineLabels;
    public Camera cam;
    public GameObject Red;
    public GameObject Orange;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Indigo;
    public GameObject Violet;
    public GameObject CardManager;

    public initializeScreen initScreen;
    public Element currentElement;
    public int CurrentLineNumber;

    void Start()
    {
        CurrentLineNumber = 0;
        
    }
    void Update()
    { 
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (Lines.Contains(hit.collider.gameObject))
                {
                    var card = CardManager.GetComponent<CardThing>().CurrentCard();
                    var kJ = CheckLines(card, hit.collider.gameObject);
                    if (kJ != null)
                    {
                        electron.transform.position = hit.collider.gameObject.transform.position;
                        CurrentLineNumber = Lines.IndexOf(hit.collider.gameObject);
                        
                        if (kJ >= 158 && kJ <= 190)
                        {
                            Debug.Log("Red");
                            Red.SetActive(true);
                        }
                        if (kJ >= 191 && kJ <= 201)
                        {
                            Debug.Log("Orange");
                            Orange.SetActive(true);
                        }
                        if (kJ >= 202 && kJ <= 210)
                        {
                            Debug.Log("Yellow");
                            Yellow.SetActive(true);
                        }
                        if (kJ >= 211 && kJ <= 238)
                        {
                            Debug.Log("Green");
                            Green.SetActive(true);
                        }
                        if (kJ >= 239 && kJ <= 245)
                        {
                            Debug.Log("Cyan");
                            Blue.SetActive(true);
                        }
                        if (kJ >= 246 && kJ <= 264)
                        {
                            Debug.Log("Blue");
                            Indigo.SetActive(true);
                        }
                        if (kJ >= 265 && kJ <= 312)
                        {
                            Debug.Log("Violet");
                            Violet.SetActive(true);
                        }
                        if (kJ > 312)
                        {
                            Debug.Log("Ultraviolet");
                        }
                        if (kJ < 158)
                        {
                            Debug.Log("Infrared");
                        }
                    }
                }
            }
        }
    }

    public int CheckLines(GameObject Card, GameObject line)
    {
        currentElement = initScreen.chosenElement;
        var electronpos = electron.transform.position;
        int CardNumber;
        int.TryParse(Card.name, out CardNumber);
        bool Checkd = false;
        int LineNumber;
        int.TryParse(line.name, out LineNumber);
        var kJ2 = currentElement.kJValues[LineNumber];
        var kJ1 = currentElement.kJValues[CurrentLineNumber];
        var kJDiff = kJ2 - kJ1;
        switch (CardNumber)
        {
            case 0:
                if (kJDiff <= 630 && kJDiff > 0)
                {
                    Checkd = true;
                }
                break;
            case 1:
                if (LineNumber == 3)
                {
                    Checkd = true;
                }
                break;
            default:
                break;
        }
        if (Checkd)
        {
            return kJDiff;
        }
        else
        {
            return 0;
        }
    }
}
