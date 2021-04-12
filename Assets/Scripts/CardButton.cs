using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{

    public List<GameObject> Cards;
   
    public void FlipFirstCard()
    {
        var FirstCard = Cards[0];
        FirstCard.GetComponent<Play>().PlzWork();
        //Child.GetComponent<PlayAnimation>().playAnimation;
        Cards.Remove(FirstCard);
    }
}
