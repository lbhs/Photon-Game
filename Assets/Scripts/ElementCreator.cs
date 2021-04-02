using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCreator : MonoBehaviour
{
    public List<Color> Colors;
    public float[,] kJvalues;
    public List<Color> Completed;
    public List<string> Combinations;
    public List<int> RedkJ;
    public List<int> OrangekJ;
    public List<int> YellowkJ;
    public List<int> GreenkJ;
    public List<int> CyankJ;
    public List<int> BluekJ;
    public List<int> VioletkJ;

    void Start()
    {
        for (int i = 158; i < 190; i++)
        {
            RedkJ.Add(i);
        }
        for (int i = 190; i < 201; i++)
        {
            OrangekJ.Add(i);
        }
        for (int i = 201; i < 210; i++)
        {
            YellowkJ.Add(i);
        }
        for (int i = 210; i < 238; i++)
        {
            GreenkJ.Add(i);
        }
        for (int i = 238; i < 245; i++)
        {
            CyankJ.Add(i);
        }
        for (int i = 245; i < 264; i++)
        {
            BluekJ.Add(i);
        }
        for (int i = 264; i < 312; i++)
        {
            VioletkJ.Add(i);
        }
        StartCoroutine("CreateElement");
    }

    IEnumerator CreateElement()
    {
        for(int x1 = 0; x1 < 313; x1++)
        {
            for(int x2 = 0; x2 < x1; x2++)
            {
                for(int x3 = 0; x3 < x2; x3++)
                {
                    if (CheckNumbers(x1, x2, x3))
                    {
                        StopCoroutine("CreateElement");
                    }
                    else
                    {
                        print(x1 + ", " + x2 + ", " + x3);
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.0f);
    }

    bool CheckNumbers(int x1, int x2, int x3)
    {
        Completed.Clear();
        Combinations.Clear();
        while (Completed.Count < 3)
        {
                if (RedkJ.Contains(x1) || OrangekJ.Contains(x1) || YellowkJ.Contains(x1) || GreenkJ.Contains(x1) || CyankJ.Contains(x1) || BluekJ.Contains(x1) || VioletkJ.Contains(x1))
                {
                    if (!Completed.Contains(Colors[i]))
                    {
                        Completed.Add(Colors[i]);
                        Combinations.Add("x1");
                    }
                }
                if (RedkJ.Contains(x2) || OrangekJ.Contains(x2) || YellowkJ.Contains(x2) || GreenkJ.Contains(x2) || CyankJ.Contains(x2) || BluekJ.Contains(x2) || VioletkJ.Contains(x2))
                {
                    if (!Completed.Contains(Colors[i]))
                    {
                        Completed.Add(Colors[i]);
                        Combinations.Add("x2");
                    }
                }
                if (RedkJ.Contains(x1 + x2) || OrangekJ.Contains(x1 + x2) || YellowkJ.Contains(x1 + x2) || GreenkJ.Contains(x1 + x2) || CyankJ.Contains(x1 + x2) || BluekJ.Contains(x1 + x2) || VioletkJ.Contains(x1 + x2))
                {
                    if (!Completed.Contains(Colors[i]))
                    {
                        Completed.Add(Colors[i]);
                        Combinations.Add("x3");
                    }
                }
                if (RedkJ.Contains(x2 + x3) || OrangekJ.Contains(x2 + x3) || YellowkJ.Contains(x2 + x3) || GreenkJ.Contains(x2 + x3) || CyankJ.Contains(x2 + x3) || BluekJ.Contains(x2 + x3) || VioletkJ.Contains(x2 + x3))
                {
                    if (!Completed.Contains(Colors[i]))
                    {
                        Completed.Add(Colors[i]);
                        Combinations.Add("x1 + x2");
                    }
                }
                if (RedkJ.Contains(x3) || OrangekJ.Contains(x3) || YellowkJ.Contains(x3) || GreenkJ.Contains(x3) || CyankJ.Contains(x3) || BluekJ.Contains(x3) || VioletkJ.Contains(x3))
                {
                    if (!Completed.Contains(Colors[i]))
                    {
                        Completed.Add(Colors[i]);
                        Combinations.Add("x2 + x3");
                    }
                }
                if (RedkJ.Contains(x1 + x2 + x3) || OrangekJ.Contains(x1 + x2 + x3) || YellowkJ.Contains(x1 + x2 + x3) || GreenkJ.Contains(x1 + x2 + x3) || CyankJ.Contains(x1 + x2 + x3) || BluekJ.Contains(x1 + x2 + x3) || VioletkJ.Contains(x1 + x2 + x3))
                {
                    if (!Completed.Contains(Colors[i]))
                    {
                        Completed.Add(Colors[i]);
                        Combinations.Add("x1 + x2 + x3");
                    }
                }
        }
        if (Completed.Count == 3)
        {
            print(x1 + ", " + x2 + ", " + x3 + "\n" + Combinations[0] + ", " + Completed[0] + "\n" + Combinations[1] + ", " + Completed[1] + "\n" + Combinations[2] + ", " + Completed[2]);
            return true;
        }
        else
        {
            return false;
        }
    }
}
