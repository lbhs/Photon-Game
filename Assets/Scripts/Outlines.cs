using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlines : MonoBehaviour
{

    public GameObject CardManager;
    public List<GameObject> LineList;
    public List<GameObject> OutlineList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        var EligibleLines = CardManager.GetComponent<CardThing>().EligibleLines1;

        foreach (GameObject line in LineList)
        {
            var index = LineList.IndexOf(line);
            var outline = OutlineList[index];
            outline.transform.position = line.transform.position;

            if (EligibleLines.Contains(line))
            {
               
                outline.SetActive(true);
            }

            else
            {
                outline.SetActive(false);
            }

        }
    }
}
