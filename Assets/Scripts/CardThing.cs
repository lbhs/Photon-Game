using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardThing : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> Cards;
    public List<GameObject> FlippedCards;
    public GameObject electron1;
    public GameObject electron2;
    public int CurrentLineNumber1;
    public int CurrentLineNumber2;
    public initializeScreen initScreen;
    public List<GameObject> EligibleLines1;
    public List<GameObject> EligibleLines2;
    public Dictionary<GameObject, int> kJDic;

    public GameObject Red;
    public GameObject Orange;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Indigo;
    public GameObject Violet;

    public void Start()
    {
        kJDic = new Dictionary<GameObject, int>();
        CurrentLineNumber1 = 0;
        CurrentLineNumber2 = 0;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (EligibleLines1.Contains(hit.collider.gameObject))
                {
                    electron1.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z - 1);
                    UpdateCurrentLine(hit.collider.gameObject, 1);
                    var kj = kJDic[hit.collider.gameObject];
                    UnityEngine.Debug.Log(kj);

                    if (kj >= -210 && kj <= -150)
                    {
                        Red.SetActive(true);
                    }
                    if (kj >= -310 && kj <= -211)
                    {
                        Orange.SetActive(true);
                    }
                    if (kj >= -410 && kj <= -311)
                    {
                        Yellow.SetActive(true);
                    }
                    if (kj >= -510 && kj <= -411)
                    {
                        Green.SetActive(true);
                    }
                    if (kj >= -610 && kj <= -511)
                    {
                        Blue.SetActive(true);
                    }
                    if (kj >= -710 && kj <= -611)
                    {
                        Indigo.SetActive(true);
                    }
                    if (kj >= -810 && kj <= -711)
                    {
                        Violet.SetActive(true);
                    }

                }
                if (EligibleLines2.Contains(hit.collider.gameObject))
                {
                    electron2.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z - 1);
                    UpdateCurrentLine(hit.collider.gameObject, 2);
                    var kj = kJDic[hit.collider.gameObject];
                    UnityEngine.Debug.Log(kj);

                    if (kj >= -210 && kj <= -158)
                    {
                        Red.SetActive(true);
                    }
                    if (kj >= -310 && kj <= -211)
                    {
                        Orange.SetActive(true);
                    }
                    if (kj >= -410 && kj <= -311)
                    {
                        Yellow.SetActive(true);
                    }
                    if (kj >= -510 && kj <= -411)
                    {
                        Green.SetActive(true);
                    }
                    if (kj >= -610 && kj <= -511)
                    {
                        Blue.SetActive(true);
                    }
                    if (kj >= -710 && kj <= -611)
                    {
                        Indigo.SetActive(true);
                    }
                    if (kj >= -810 && kj <= -711)
                    {
                        Violet.SetActive(true);
                    }

                }
            }
        }
    }

    public void FlipFirstCard()
    {
        var CardNumber = Random.Range(0, 11);
        EligibleLines1 = CheckLines(initScreen.levels, initScreen.chosenElement, CardNumber, CurrentLineNumber1);
        EligibleLines2 = CheckLines(initScreen.levels2, initScreen.chosenElement2, CardNumber, CurrentLineNumber2);

        if (EligibleLines1.Count() == 0 && EligibleLines2.Count() == 0)
        {
            UnityEngine.Debug.Log("no possible lines for card " + CardNumber);
            FlipFirstCard();
        }
        else
        {
            foreach(GameObject card in FlippedCards)
            {
                var pos = card.transform.position;
                card.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
            }
            Transform newcard = Instantiate(Cards[CardNumber].transform, new Vector3(-6, 1, 0), new Quaternion(0, 0, 0, 0), this.transform);
            newcard.gameObject.GetComponentInChildren<Animation>().Play("yuhh");
            FlippedCards.Add(newcard.gameObject);
        }
    }

    public List<GameObject> CheckLines(List<GameObject> Linelist, Element element, int CardNumber, int CurrentLineNumber)
    {
        List<GameObject> ReturnLines = new List<GameObject>();

        foreach (GameObject line in Linelist)
        {
            if (line == Linelist[CurrentLineNumber])
            {
                continue;
            }

            int LineNumber = Linelist.IndexOf(line);
            var kJ2 = element.kJValues[LineNumber];
            var kJ1 = element.kJValues[CurrentLineNumber];
            var kJDiff = kJ2 - kJ1;
            switch (CardNumber)
            {
                case 0:
                    if (kJDiff <= 630 && kJDiff > 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 1:
                    if (LineNumber == 2)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 2:
                    if (LineNumber == 1)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 3:
                    if (kJDiff <= 700 && kJDiff >= 300)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 4:
                    if (kJDiff < 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 5:
                    if (LineNumber == 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 6:
                    if (kJDiff <= 300 && kJDiff >= 170)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 7:
                    if (kJDiff > 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 8:
                    if (CurrentLineNumber == 1)
                    {
                        if (LineNumber == 0)
                        {
                            ReturnLines.Add(line);
                            kJDic[line] = kJDiff;
                        }
                    }
                    else
                    {
                        if (LineNumber == (CurrentLineNumber - 2))
                        {
                            ReturnLines.Add(line);
                            kJDic[line] = kJDiff;
                        }
                    }
                    break;
                case 9:
                    if (kJDiff <= 700 && kJDiff >= 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 10:
                    if (kJDiff <= 800 && kJDiff >= 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                default:
                    break;
            }
        }
        return ReturnLines;
    }

    public void UpdateCurrentLine(GameObject line, int number)
    {
        if (number == 1)
        {
            CurrentLineNumber1 = initScreen.levels.IndexOf(line);
            UnityEngine.Debug.Log("Current Line1 = " + CurrentLineNumber1);
        }
        if (number == 2)
        {
            CurrentLineNumber2 = initScreen.levels2.IndexOf(line);
            UnityEngine.Debug.Log("Current Line2 = " + CurrentLineNumber2);
        }
        EligibleLines1.Clear();
        EligibleLines2.Clear();
    }

}
