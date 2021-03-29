using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{

    public List<GameObject> Cards;
    public void FlipCard()
    {
        this.GetComponent<Animation>().Play("yuhh");
        Card.removeAt(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
