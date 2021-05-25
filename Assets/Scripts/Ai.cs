using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Ai : MonoBehaviour
{
    public CardThing CardThingScript;
    public initializeScreen initScreen;
    public int CardNumber;
    public List<Card> CardsInOrder;
    public List<GameObject> PossibleLines;
    public Dictionary<GameObject, GameObject> LineCombos;

    [System.Serializable]
    public class Card
    {
   //     public List<int> kJBounds;
 //       public int targetline;
        public GameObject CardObject;
    }

    public int PickACard()
    {
        List<Card> CopyCardList = new List<Card>();
        CopyCardList = CardsInOrder;
        CardNumber = -1;
        /* This function will pick a card by filtering the list of cards through a 
         * few parameters and also by accounting for certain situations. The initial parameters 
         * remove cards from the list of possible cards, the latter parameters pick a card from
         * the list and assign it to CardNumber. The parameters are in order of how important they are, 
         * so that the last parameters, which are more important, will overrite what CardNumber is. In the end it returns CardNumber
         * for the CardThing Script to use. */

        /* Go through all the cards and remove the ones that are a repeat of the last card
         * or only create 0 or 1 possible moves */

        foreach(Card c in CardsInOrder)
        {
            if((CardsInOrder.IndexOf(c)) == CardThingScript.LastCard)
            {
                CopyCardList.Remove(c);
            }
            var yuh = CardThingScript.CheckLines(CardThingScript.wells[0], CardsInOrder.IndexOf(c));
            var yuh2 = CardThingScript.CheckLines(CardThingScript.wells[1], CardsInOrder.IndexOf(c));
            if ( (yuh.Count() + yuh2.Count()) < 2)
            {
                CopyCardList.Remove(c);
            }
        }

        /* If the electrons are towards the top of the well, pick a card in the first half of the list,
         * which will probably be a card that makes the electrons lose energy, and vice versa for 
         * if the electrons are at the bottom */
        if ((CardThingScript.wells[0].CurrentLineNumber + CardThingScript.wells[1].CurrentLineNumber) >= 6)
        {
            CardNumber = Random.Range(0, (int)Mathf.Ceil((CopyCardList.Count() / 2)));
        }
        if ((CardThingScript.wells[0].CurrentLineNumber + CardThingScript.wells[1].CurrentLineNumber) <= 2)
        {
            CardNumber = Random.Range((int)Mathf.Floor(CopyCardList.Count() / 2), CopyCardList.Count());
        }

        /* If there are 2 or less colors left, this part of the function will pick a card that either has
         * the possibility of creating one of those colors or a card that can get the player to where 
         * they need to be to create the color */

        if (CardThingScript.CompletedColors.Count() >= 7)
        {
            CalculatePossibleLines();

            /* This part calls the CheckPossibleLinesAgainstCurrentLines function, and if it returns a card,
             * it sets CardNumber equal to that cards index. You can read more about the function in the 
             * actual function. Otherwise, it calls PickACardToGetToPossibleLine, which does exactly what the name says */

            var returncard = CheckPossibleLinesAgainstCurrentLines(CopyCardList);
            if (returncard != null)
            {
                CardNumber = CardsInOrder.IndexOf(returncard);
            }
            else
            {
                var otherreturncard = PickACardToGetToPossibleLine(CopyCardList);
                if (otherreturncard != null)
                {
                    CardNumber = CardsInOrder.IndexOf(otherreturncard);
                }
            }
        }

        /* Check to make sure we assigned a card, and if not just assign a random one */

        if(CardNumber == -1)
        {
            CardNumber = CopyCardList.IndexOf(CopyCardList[Random.Range(0, CopyCardList.Count())]);
        }
        return CardNumber;
    }

    public void CalculatePossibleLines()
    {
        /* This function goes through every combination of lines to find which 
         * ones create the desired colors, and then assigns those lines to a list
         * and assigns the line pair that completes the color in a dictionary, 
         * with the top one being the key and the bottom one the value. 
         * First it checks to make sure the list hasn't already been created, so that 
         * when the function is called repeatedly on differnet turns, it won't calculate 
         * everything all over again. */

        if (PossibleLines.Count() == 0)
        {
            foreach (CardThing.Well well in CardThingScript.wells)
            {
                foreach (GameObject line1 in well.levellist)
                {
                    foreach (GameObject line2 in well.levellist)
                    {
                        var kj = well.element.kJValues[well.levellist.IndexOf(line1)] - well.element.kJValues[well.levellist.IndexOf(line2)];
                        foreach (CardThing.aColor col in CardThingScript.colorss)
                        {
                            if (!CardThingScript.CompletedColors.Contains(col.ColorObject) && (kj > col.ColorBounds[0] && kj < col.ColorBounds[1]))
                            {
                                PossibleLines.Add(line1);
                                LineCombos[line1] = line2;
                            }
                        }
                    }
                }
            }
        }
    }

    public Card CheckPossibleLinesAgainstCurrentLines(List<Card> CopyCardList)
    {
        /* This function is designed to check each turn if any of the lines in PossibleLines
         * are one of the current lines that an electron is on. If so, it figures out
         * which of the cards will get the player to the lower line in the line combo and 
         * thus complete the color, and assigns the cards to a dictionary. Then it randomly chooses
         * one of the possible lines that is a current line (if there are any) and randomly
         * chooses one of the cards from the dictionary associated with it, and returns that Card */

        List<GameObject> PossibleLinesThatAreCurrentLines = new List<GameObject>();
        Dictionary<GameObject, List<Card>> LineCardOptions = new Dictionary<GameObject, List<Card>>();
        foreach (GameObject line in PossibleLines)
        {
            List<Card> PossibleCards = new List<Card>();
            if (CardThingScript.wells[0].CurrentLineNumber == CardThingScript.wells[0].levellist.IndexOf(line))
            {
                foreach (Card c in CopyCardList)
                {
                    var yuh = (CardThingScript.CheckLines(CardThingScript.wells[0], CardsInOrder.IndexOf(c)));
                    if (yuh.Contains(LineCombos[line]))
                    {
                        PossibleCards.Add(c);
                    }
                }
                LineCardOptions[line] = PossibleCards;
                PossibleLinesThatAreCurrentLines.Add(line);
            }
            if (CardThingScript.wells[1].CurrentLineNumber == CardThingScript.wells[1].levellist.IndexOf(line))
            {
                foreach (Card c in CopyCardList)
                {
                    var yuh = (CardThingScript.CheckLines(CardThingScript.wells[1], CardsInOrder.IndexOf(c)));
                    if (yuh.Contains(LineCombos[line]))
                    {
                        PossibleCards.Add(c);
                    }
                }
                LineCardOptions[line] = PossibleCards;
                PossibleLinesThatAreCurrentLines.Add(line);
            }
        }
        if (PossibleLinesThatAreCurrentLines.Count() != 0)
        {
            var index = Random.Range(0, PossibleLinesThatAreCurrentLines.Count());
            var index2 = Random.Range(0, LineCardOptions[PossibleLinesThatAreCurrentLines[index]].Count());
            return LineCardOptions[PossibleLinesThatAreCurrentLines[index]][index2];
        }
        else
        {
            return null;
        }
    }

    public Card PickACardToGetToPossibleLine(List<Card> CopyCardList)
    {
        /* This function picks a card randomly that makes it possible to get to one of the PossibleLines */

        List<Card> PossibleCards = new List<Card>();
        foreach (GameObject line in PossibleLines)
        {
            foreach (Card c in CopyCardList)
            {
                var yuh = (CardThingScript.CheckLines(CardThingScript.wells[0], CardsInOrder.IndexOf(c)));
                var yuh2 = (CardThingScript.CheckLines(CardThingScript.wells[1], CardsInOrder.IndexOf(c)));
                if (yuh.Contains(line) || yuh2.Contains(line))
                {
                    PossibleCards.Add(c);
                }
            }
        }
        if (PossibleCards.Count() != 0)
        {
            var index = Random.Range(0, PossibleCards.Count());
            return PossibleCards[index];
        }
        else
        {
            return null;
        }
    }
}
