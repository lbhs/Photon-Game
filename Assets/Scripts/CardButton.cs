using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{

    public List<GameObject> Cards;
   
    public void FlipFirstCard()
    {
        var FirstCard = Cards[0];
        Animation animation = FirstCard.GetComponentInChildren<Animation>();
        Debug.Log(animation);
        animation.Play("yuhh", PlayMode.StopAll);
        Cards.Remove(FirstCard);
    }
}
