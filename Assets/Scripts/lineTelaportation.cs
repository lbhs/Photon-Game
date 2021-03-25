using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTelaportation : MonoBehaviour
{
    public List<GameObject> Lines;
    public List<Vector3> LinePositions;
    public GameObject electron;


    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject line in Lines)
        {
            var lineposition = line.transform.position;
            LinePositions.Add(lineposition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << 8;

            if(Physics.Raycast(ray, out hit, 1000, layerMask))
            {
                electron.transform.position = hit.transform.position;
            }
        }
    }
}
