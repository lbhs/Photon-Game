using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardThing : MonoBehaviour
{
    public List<GameObject> Cards;
    public List<GameObject> CardsForIndex;

    public List<GameObject> FlippedCards;
    public GameObject LineManager;

    public List<GameObject> Lines;
    public GameObject electron;
    public int CurrentLineNumber;
    public initializeScreen initScreen;
    public Element currentElement;
    public List<GameObject> EligibleLines;
    public Dictionary<GameObject, int> kJDic;

    public void Start()
    {
        kJDic = new Dictionary<GameObject, int>();
        CurrentLineNumber = 0;
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
        EligibleLines = CheckLines();
    }

    public List<GameObject> CheckLines()
    {
        currentElement = initScreen.chosenElement;
        var electronpos = electron.transform.position;
        var currentcard = FlippedCards.Last();
        var CardNumber = CardsForIndex.IndexOf(currentcard);
        UnityEngine.Debug.Log("Card Number: " + CardNumber);
        List<GameObject> ReturnLines = new List<GameObject>();
        foreach (GameObject line in Lines)
        {
            int LineNumber = Lines.IndexOf(line);
            var kJ2 = currentElement.kJValues[LineNumber];
            var kJ1 = currentElement.kJValues[CurrentLineNumber];
            var kJDiff = kJ2 - kJ1;
            switch (CardNumber)
            {
                case 0:
                    if (kJDiff <= 1000 && kJDiff > 0)
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
        if (ReturnLines.Count() > 0)
        {
            return ReturnLines;
        }
        else
        {
            return null;
        }
    }

    public void UpdateCurrentLine(int linenumber)
    {
        CurrentLineNumber = linenumber;
        EligibleLines = null;
    }

}
