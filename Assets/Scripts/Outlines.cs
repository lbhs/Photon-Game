using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outlines : MonoBehaviour
{

    public GameObject CardManager;
    public List<GameObject> LineList;
    public List<GameObject> OutlineList;
    public List<GameObject> LineList2;
    public List<GameObject> OutlineList2;
    //public GameObject CardOutline;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var EligibleLines = CardManager.GetComponent<CardThing>().wells[0].EligibleLines;

        foreach (GameObject line in LineList)
            //brings the outlines to match the position of each line in the well (for well 1)
        {
            var index = LineList.IndexOf(line);
            var outline = OutlineList[index];
            outline.transform.position = new Vector3(line.transform.position.x, line.transform.position.y, line.transform.position.z + 0.5f);

            if (EligibleLines.Contains(line))
                //turns on the outlines if a line is contained in the Eligible Lines list
            {
               
                outline.SetActive(true);
            }

            else
            {
                outline.SetActive(false);
            }

        }
        //for tutorial
        if (selectElement.elementNames != "Hydrogen") {
            var EligibleLines2 = CardManager.GetComponent<CardThing>().wells[1].EligibleLines;

        //for well 2
            foreach (GameObject line in LineList2)
            {
                var index = LineList2.IndexOf(line);
                var outline = OutlineList2[index];
                outline.transform.position = new Vector3(line.transform.position.x, line.transform.position.y, line.transform.position.z + 0.5f);

                if (EligibleLines2.Contains(line))
                {
                    outline.SetActive(true);
                }

                else
                {
                    outline.SetActive(false);
                }
            }

            /*if ((EligibleLines.Count == 0) && (EligibleLines2.Count == 0))
            {
                CardOutline.SetActive(true);
            }*/
        }
    }
        
}
