using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTelaportation : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject Line in Lines)
        {
 
            var pos = new Vector3(Line.transform.position.x + 0, Line.transform.position.y + 0, Line.transform.position.z);
            var newpos = cam.WorldToScreenPoint(pos);
            var index = Lines.IndexOf(Line);
            var text = lineLabels[index];
            text.transform.position = newpos;
        }
        


        if(Input.GetMouseButtonDown(0))
        {
            UnityEngine.Debug.Log("pressed");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          //  int layerMask = 1 << 8;

            if(Physics.Raycast(ray, out hit, 1000))
            {
                UnityEngine.Debug.Log("hit");
                if (Lines.Contains(hit.collider.gameObject))
                {
                    Vector3 lastPos = electron.transform.position;
                    Vector3 newPos = hit.collider.gameObject.transform.position;


                    if (lastPos.y > newPos.y)
                    {
                        Debug.Log((int)((lastPos.y + 2.91f) * 160f - (newPos.y + 2.91) * 160));
                        var kJDifference = ((int)((lastPos.y + 2.91f) * 160f - (newPos.y + 2.91) * 160));

                        if (kJDifference >= 158 && kJDifference <= 190)
                        {
                            Debug.Log("Red");
                            Red.SetActive(true);
                        }
                        if (kJDifference >= 191 && kJDifference <= 201)
                        {
                            Debug.Log("Orange");
                            Orange.SetActive(true);
                        }
                        if (kJDifference >= 202 && kJDifference <= 210)
                        {
                            Debug.Log("Yellow");
                            Yellow.SetActive(true);
                        }
                        if (kJDifference >= 211 && kJDifference <= 238)
                        {
                            Debug.Log("Green");
                            Green.SetActive(true);
                        }
                        if (kJDifference >= 239 && kJDifference <= 245)
                        {
                            Debug.Log("Cyan");
                            Blue.SetActive(true);
                        }
                        if (kJDifference >= 246 && kJDifference <= 264)
                        {
                            Debug.Log("Blue");
                            Indigo.SetActive(true);
                        }
                        if (kJDifference >= 265 && kJDifference <= 312)
                        {
                            Debug.Log("Violet");
                            Violet.SetActive(true);
                        }
                        if (kJDifference > 312)
                        {
                            Debug.Log("Ultraviolet");
                            
                        }
                        if (kJDifference < 158)
                        {
                            Debug.Log("Infrared");
                            
                        }
                    }

                    electron.transform.position = hit.collider.gameObject.transform.position;
                }
            }
        }
    }
}
