using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Ai : MonoBehaviour
{
    public CardThing card;
    public initializeScreen initScreen;
    public int CardNumber;
    public List<Card> CardsInOrder;

    [System.Serializable]
    public class Card
    {
        public List<int> kJBounds;
        public int targetline;
        public int Actual;
        public GameObject CardObject;
    }

    public int PickACard()
    {
        List<Card> CopyCardList = new List<Card>();
        CopyCardList = CardsInOrder;
        foreach(Card c in CopyCardList)
        {
            if((CopyCardList.IndexOf(c)) == card.LastCard)
            {
                CopyCardList.Remove(c);
            }
            var yuh = card.CheckLines(card.wells[0], CopyCardList.IndexOf(c));
            var yuh2 = card.CheckLines(card.wells[1], CopyCardList.IndexOf(c));
            if ( (yuh.Count() + yuh2.Count()) < 2)
            {
                CopyCardList.Remove(c);
            }
        }
        if ((card.wells[0].CurrentLineNumber + card.wells[1].CurrentLineNumber) >= 6)
        {
            CardNumber = Random.Range(0, (int)Mathf.Ceil((CopyCardList.Count() / 2) + 1));
        }
        if ((card.wells[0].CurrentLineNumber + card.wells[1].CurrentLineNumber) <= 2)
        {
            CardNumber = Random.Range((int)Mathf.Floor(CopyCardList.Count() / 2), (int)Mathf.Floor(CopyCardList.Count() + 1));
        }
        if (card.CompletedColors.Count() >= 7)
        {
            List<GameObject> PosLines = new List<GameObject>();
            foreach (Well well in card.wells)
            {
                foreach (GameObject line1 in well.levellist)
                {
                    foreach (GameObject line2 in well.levellist)
                    {
                        kj = well.element.kJValues[well.levellist.IndexOf(line1)] - well.element.kJValues[well.levellist.IndexOf(line2)];
                        foreach (aColor col in card.colorss)
                        {
                            if (!card.CompletedColors.Contains(col) && (kj > col.ColorBounds[0] && kj < col.ColorBounds[1]))
                            {
                                PosLines.Add(line1);
                            }
                        }
                    }
                }
            }
            if(card.wells[0].CurrentLineNumber == )
            foreach(Card c in CopyCardList)
            {

            }
        }
    }
}
