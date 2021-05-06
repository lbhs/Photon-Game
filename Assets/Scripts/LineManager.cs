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
                if (CardManager.GetComponent<CardThing>().EligibleLines1.Contains(hit.collider.gameObject) || CardManager.GetComponent<CardThing>().EligibleLines2.Contains(hit.collider.gameObject))
                {
                        electron.transform.position = hit.collider.gameObject.transform.position;
                        CardManager.GetComponent<CardThing>().UpdateCurrentLine(hit.collider.gameObject);
                        var kj = CardManager.GetComponent<CardThing>().kJDic[hit.collider.gameObject];
                        UnityEngine.Debug.Log(kj);
                        
                        if(kj >= -210 && kj <= -158)
                        {
                            Red.SetActive(true);
                        }
                        if (kj >= -310 && kj <= -211)
                        {
                            Orange.SetActive(true);
                        }
                        if (kj >= -410 && kj <= -311)
                        {
                            Yellow.SetActive(true);
                        }
                        if (kj >= -510 && kj <= -411)
                        {
                            Green.SetActive(true);
                        }
                        if (kj >= -610 && kj <= -511)
                        {
                            Blue.SetActive(true);
                        }
                        if (kj >= -710 && kj <= -611)
                        {
                            Indigo.SetActive(true);
                        }
                        if (kj >= -810 && kj <= -711)
                        {
                            Violet.SetActive(true);
                        }
                    
                }
            }
        }
    }
}
