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
                    UnityEngine.Debug.Log("hit line");
                    var cardnumber = CardManager.GetComponent<CardThing>().CurrentCard();
                    var kJ = CheckLines(cardnumber, hit.collider.gameObject);
                    if (kJ != 0)
                    {
                        electron.transform.position = hit.collider.gameObject.transform.position;
                        CurrentLineNumber = Lines.IndexOf(hit.collider.gameObject);
                        
                        if (kJ >= -210 && kJ <= -158)
                        {
                            Debug.Log("Red");
                            Red.SetActive(true);
                        }
                        if (kJ >= -310 && kJ <= -211)
                        {
                            Debug.Log("Orange");
                            Orange.SetActive(true);
                        }
                        if (kJ >= -410 && kJ <= -311)
                        {
                            Debug.Log("Yellow");
                            Yellow.SetActive(true);
                        }
                        if (kJ >= -510 && kJ <= -411)
                        {
                            Debug.Log("Green");
                            Green.SetActive(true);
                        }
                        if (kJ >= -610 && kJ <= -511)
                        {
                            Debug.Log("Cyan");
                            Blue.SetActive(true);
                        }
                        if (kJ >= -710 && kJ <= -611)
                        {
                            Debug.Log("Blue");
                            Indigo.SetActive(true);
                        }
                        if (kJ >= -810 && kJ <= -711)
                        {
                            Debug.Log("Violet");
                            Violet.SetActive(true);
                        }
                        if (kJ < -810)
                        {
                            Debug.Log("Ultraviolet");
                        }
                        if (kJ > -158 && kJ < 0)
                        {
                            Debug.Log("Infrared");
                        }
                    }
                }
            }
        }
    }

    public int CheckLines(int CardNumber, GameObject line)
    {
        currentElement = initScreen.chosenElement;
        var electronpos = electron.transform.position;
        UnityEngine.Debug.Log("Card Number: " + CardNumber);
        bool Checkd = false;
        int LineNumber = Lines.IndexOf(line);
        UnityEngine.Debug.Log("LineNumber: " + LineNumber);
        var kJ2 = currentElement.kJValues[LineNumber];
        var kJ1 = currentElement.kJValues[CurrentLineNumber];
        var kJDiff = kJ2 - kJ1;
        UnityEngine.Debug.Log("kJ: " + kJDiff);
        switch (CardNumber)
        {
            case 0:
                if (kJDiff <= 1000 && kJDiff > 0)
                {
                    Checkd = true;
                }
                break;
            case 1:
                if (LineNumber == 2)
                {
                    Checkd = true;
                }
                break;
            case 2:
                if (LineNumber == 1)
                {
                    Checkd = true;
                }
                break;
            case 3:
                if (kJDiff <= 700 && kJDiff >= 300)
                {
                    Checkd = true;
                }
                break;
            case 4:
                if (kJDiff < 0)
                {
                    Checkd = true;
                }
                break;
            case 5:
                if (LineNumber == 0)
                {
                    Checkd = true;
                }
                break;
            case 6:
                if (kJDiff <= 300 && kJDiff >= 170)
                {
                    Checkd = true;
                }
                break;
            case 7:
                if (kJDiff > 0)
                {
                    Checkd = true;
                }
                break;
            case 8:
                if (CurrentLineNumber == 1)
                {
                    if (LineNumber == 0)
                    {
                        Checkd = true;
                    }
                }
                else
                {
                    if (LineNumber == (CurrentLineNumber - 2))
                    {
                        Checkd = true;
                    }
                }
                break;
            case 9:
                if (kJDiff <= 700 && kJDiff >= 0)
                {
                    Checkd = true;
                }
                break;
            case 10:
                if (kJDiff <= 800 && kJDiff >= 0)
                {
                    Checkd = true;
                }
                break;
            default:
                break;
        }
        if (Checkd)
        {
            UnityEngine.Debug.Log("checkd");
            return kJDiff;
        }
        else
        {
            UnityEngine.Debug.Log("failed");
            return 0;
        }
    }
}
