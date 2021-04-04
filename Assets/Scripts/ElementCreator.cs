using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCreator : MonoBehaviour
{
    public List<Color> Completed;
    public List<string> Combinations;

    void Start()
    {
        StartCoroutine("CreateElement");
    }

    IEnumerator CreateElement()
    {
  //      while (Completed.Count < 3)
 //       {
        //    var x1 = Random.Range(0, 313);
        //    var x2 = Random.Range(0, x1);
         //   var x3 = Random.Range(0, x2);
            for (int x1 = 245; x1 < 313; x1++)
            {
                for (int x2 = 0; x2 < x1; x2++)
                {
                    for (int x3 = 0; x3 < x2; x3++)
                    {
                        if (CheckNumbers(x1, x2, x3))
                        {
                            print("Found Element");
          //                  StopCoroutine("CreateElement");
                        }
                        else
                        {
                            print(x1 + ", " + x2 + ", " + x3);
                            yield return new WaitForSeconds(0.01f);
                        }
                    }
                }
            }
            

//        }
        yield return new WaitForSeconds(0.0f);
    }

    bool CheckNumbers(int x1, int x2, int x3)
    {
        Completed.Clear();
        Combinations.Clear();
        while (Completed.Count < 3)
        {
                if (CheckColors(x1) != Color.black)
                {
                    var color = CheckColors(x1);
                    if (!Completed.Contains(color))
                    {
                        Completed.Add(color);
                        Combinations.Add("x1");
                    }
                }
                if (CheckColors(x2) != Color.black)
                {
                    var color = CheckColors(x2);
                    if (!Completed.Contains(color))
                    {
                        Completed.Add(color);
                        Combinations.Add("x2");
                    }
                }
                if (CheckColors(x1 + x2) != Color.black)
                {
                    var color = CheckColors(x1 + x2);
                    if (!Completed.Contains(color))
                    {
                        Completed.Add(color);
                        Combinations.Add("x3");
                    }
                }
                if (CheckColors(x3 + x2) != Color.black)
                {
                    var color = CheckColors(x3 + x2);
                    if (!Completed.Contains(color))
                    {
                        Completed.Add(color);
                        Combinations.Add("x1 + x2");
                    }
                }
                if (CheckColors(x3) != Color.black)
                {
                    var color = CheckColors(x3);
                    if (!Completed.Contains(color))
                    {
                        Completed.Add(color);
                        Combinations.Add("x2 + x3");
                    }
                }
                if (CheckColors((x1 + x2 + x3)) != Color.black)
                {
                    var color = CheckColors(x1 + x2 + x3);
                    if (!Completed.Contains(color))
                    {
                        Completed.Add(color);
                        Combinations.Add("x1 + x2 + x3");
                    }
                }
                else
                {
                    break;
                }
        }
        if (Completed.Count > 3)
        {
            print(x1 + ", " + x2 + ", " + x3 + "\n" + Combinations[0] + ", " + Completed[0] + "\n" + Combinations[1] + ", " + Completed[1] + "\n" + Combinations[2] + ", " + Completed[2]);
            return true;
        }
        else
        {
            return false;
        }
    }

    Color CheckColors(int i)
    {
        if (i >= 158 && i < 190)
        {
            return Color.red;
        }
        if (i >= 190 && i < 201)
        {
            return Color.clear;
        }
        if (i >= 201 && i < 210)
        {
            return Color.yellow;
        }
        if (i >= 210 && i < 238)
        {
            return Color.green;
        }
        if (i >= 238 && i < 245)
        {
            return Color.cyan;
        }
        if (i >= 245 && i < 264)
        {
            return Color.blue;
        }
        if (i >= 264 && i < 312)
        {
            return Color.magenta;
        }
        else
        {
            return Color.black;
        }
    }
}
