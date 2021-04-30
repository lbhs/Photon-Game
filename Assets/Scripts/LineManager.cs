using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public List<GameObject> Lines;
    public GameObject electron;
    public List<GameObject> lineLabels;
    public Camera cam;
    public GameObject Red;
    public GameObject Orange;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Indigo;
    public GameObject Violet;
    public GameObject CardManager;

    void Start()
    {
        
    }
    void Update()
    { 
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 1000))
            {
                if (Lines.Contains(hit.collider.gameObject))
                {
                    if (CardManager.GetComponent<CardThing>().EligibleLines.Contains(hit.collider.gameObject))
                    {
                        electron.transform.position = hit.collider.gameObject.transform.position;
                        CardManager.GetComponent<CardThing>().UpdateCurrentLine(Lines.IndexOf(hit.collider.gameObject));
                    }
                }
            }
        }
    }
}
