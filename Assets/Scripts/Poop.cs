using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    public List<GameObject> Cards;

    public void FlipFirstCard()
    {
        var FirstCard = Cards[0];
        Animation animation = FirstCard.GetComponentInChildren<Animation>();
        animation.Play("yuhh");
        var pos = FirstCard.transform.position;
        FirstCard.transform.position = new Vector3(pos.x, pos.y, 0 - pos.z);
        Cards.Remove(FirstCard);
    }
}
