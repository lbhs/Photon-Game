using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTelaportation : MonoBehaviour
{
    public List<GameObject> Lines;
    public GameObject electron;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
                        Debug.Log(lastPos.y - newPos.y);
                    }

                    electron.transform.position = hit.collider.gameObject.transform.position;
                }
            }
        }
    }
}
