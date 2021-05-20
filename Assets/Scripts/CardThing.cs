using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardThing : MonoBehaviour
{
    public Camera cam;

    public List<GameObject> Cards;
    public List<GameObject> CardsForIndex;

    public List<GameObject> FlippedCards;
    public GameObject LineManager;

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

    public AudioSource red;
    public AudioSource orange;
    public AudioSource yellow;
    public AudioSource green;
    public AudioSource blue;
    public AudioSource indigo;
    public AudioSource violet;

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
                    electron1.transform.position = hit.collider.gameObject.transform.position;
                    UpdateCurrentLine(hit.collider.gameObject, 1);
                    var kj = kJDic[hit.collider.gameObject];
                    UnityEngine.Debug.Log(kj);

                    if (kj >= -210 && kj <= -158)
                    {
                        Red.SetActive(true);
                        red.Play();
                    }
                    if (kj >= -310 && kj <= -211)
                    {
                        Orange.SetActive(true);
                        orange.Play();
                    }
                    if (kj >= -410 && kj <= -311)
                    {
                        Yellow.SetActive(true);
                        yellow.Play();
                    }
                    if (kj >= -510 && kj <= -411)
                    {
                        Green.SetActive(true);
                        green.Play();

                    }
                    if (kj >= -610 && kj <= -511)
                    {
                        Blue.SetActive(true);
                        blue.Play();

                    }
                    if (kj >= -710 && kj <= -611)
                    {
                        Indigo.SetActive(true);
                        indigo.Play();

                    }
                    if (kj >= -810 && kj <= -711)
                    {
                        Violet.SetActive(true);
                        violet.Play();

                    }

                }
                if (EligibleLines2.Contains(hit.collider.gameObject))
                {
                    electron2.transform.position = hit.collider.gameObject.transform.position;
                    UpdateCurrentLine(hit.collider.gameObject, 2);
                    var kj = kJDic[hit.collider.gameObject];
                    UnityEngine.Debug.Log(kj);

                    if (kj >= -210 && kj <= -158)
                    {
                        Red.SetActive(true);
                        red.Play();
                    }
                    if (kj >= -310 && kj <= -211)
                    {
                        Orange.SetActive(true);
                        orange.Play();
                    }
                    if (kj >= -410 && kj <= -311)
                    {
                        Yellow.SetActive(true);
                        yellow.Play();

                    }
                    if (kj >= -510 && kj <= -411)
                    {
                        Green.SetActive(true);
                        green.Play();

                    }
                    if (kj >= -610 && kj <= -511)
                    {
                        Blue.SetActive(true);
                        blue.Play();

                    }
                    if (kj >= -710 && kj <= -611)
                    {
                        Indigo.SetActive(true);
                        indigo.Play();

                    }
                    if (kj >= -810 && kj <= -711)
                    {
                        Violet.SetActive(true);
                        violet.Play();

                    }

                }
            }
        }
    }

    public void FlipFirstCard()
    {
        var FirstCard = Cards[0];
        Animation animation = FirstCard.GetComponentInChildren<Animation>();
        animation.Play("yuhh");
        var pos = FirstCard.transform.position;
        FirstCard.transform.position = new Vector3(pos.x, pos.y, 0 - pos.z);
        Cards.Remove(FirstCard);
        FlippedCards.Add(FirstCard);

        var currentcard = FlippedCards.Last();
        var CardNumber = CardsForIndex.IndexOf(currentcard);
        EligibleLines1 = CheckLines(initScreen.levels, initScreen.chosenElement, CardNumber, CurrentLineNumber1);
        EligibleLines2 = CheckLines(initScreen.levels2, initScreen.chosenElement2, CardNumber, CurrentLineNumber2);
        foreach (GameObject line in EligibleLines1)
        {
            UnityEngine.Debug.Log(line.name);
        }
        foreach (GameObject line in EligibleLines2)
        {
            UnityEngine.Debug.Log(line.name);
        }
    }

    public List<GameObject> CheckLines(List<GameObject> Linelist, Element element, int CardNumber, int CurrentLineNumber)
    {
        List<GameObject> ReturnLines = new List<GameObject>();

        foreach (GameObject line in Linelist)
        {
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
