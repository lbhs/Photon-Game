using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardThing : MonoBehaviour
{
    public List<GameObject> Cards;
    public List<GameObject> FlippedCards;
    public GameObject LineManager;

    public void FlipFirstCard()
    {
        var FirstCard = Cards[0];
        Animation animation = FirstCard.GetComponentInChildren<Animation>();
        animation.Play("yuhh");
        var pos = FirstCard.transform.position;
        FirstCard.transform.position = new Vector3(pos.x, pos.y, 0 - pos.z);
        Cards.Remove(FirstCard);
        FlippedCards.Add(FirstCard);
    }

    public GameObject CurrentCard()
    {
        Card = FlippedCards.Last();
        return Cards;
    }
}
