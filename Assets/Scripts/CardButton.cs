using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{

    public List<GameObject> Cards;
   
    public void FlipFirstCard()
    {
        var FirstCard = Cards[0];
        var transform = FirstCard.transform;
        var Child = transform.GetChild(0);
        Debug.Log(Child);
        Child.GetComponent<PlayAnimation>().playAnimation;
        Cards.Remove(FirstCard);
    }
}
