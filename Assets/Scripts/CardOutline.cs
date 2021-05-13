using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOutline : MonoBehaviour

{

    public GameObject Outline;
    public GameObject Card11;
    public Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        Outline.transform.position = Cam.WorldToScreenPoint(Card11.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
