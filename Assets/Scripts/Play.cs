using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public GameObject Child;
    public void PlzWork()
    {
        Child.GetComponent<Animation>().Play("yuhh");
    }
}
