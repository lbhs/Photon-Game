using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardThing : MonoBehaviour
{
    public Camera cam;
    public List<GameObject> Cards;
    public List<GameObject> FlippedCards;
    public GameObject electron1;
    public GameObject electron1parent;
    public GameObject electron2;
    public GameObject electron2parent;
    public int CurrentLineNumber1;
    public int CurrentLineNumber2;
    public initializeScreen initScreen;
    public List<GameObject> EligibleLines1;
    public List<GameObject> EligibleLines2;
    public Dictionary<GameObject, int> kJDic;
    public int LastCard;
    public int pog;
    public Text scoretext;
    public float FadeSpeed;

    public GameObject Red;
    public GameObject Orange;
    public GameObject Yellow;
    public GameObject Green;
    public GameObject Blue;
    public GameObject Indigo;
    public GameObject Violet;

    public AudioSource red;
    public AudioSource orange;
    public AudioSource yellow;
    public AudioSource green;
    public AudioSource blue;
    public AudioSource indigo;
    public AudioSource violet;

    public void Start()
    {
        kJDic = new Dictionary<GameObject, int>();
        CurrentLineNumber1 = 0;
        CurrentLineNumber2 = 0;
        LastCard = -1;
        pog = -1;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (EligibleLines1.Contains(hit.collider.gameObject))
                {
                    electron1parent.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + .035f, hit.collider.gameObject.transform.position.z - 1);
                    CurrentLineNumber1 = initScreen.levels.IndexOf(hit.collider.gameObject);
                    EligibleLines1.Clear();
                    EligibleLines2.Clear();
                    if (CurrentLineNumber1 == 0)
                    {
                        electron1.GetComponent<Animation>().Stop("wiggle");
                        
                    }
                    else
                    {
                        electron1.GetComponent<Animation>().Play("wiggle");
                        electron1.GetComponent<Animation>()["wiggle"].speed = .25f + .15f * CurrentLineNumber1;
                    }


                    var kj = kJDic[hit.collider.gameObject];
                    UnityEngine.Debug.Log(kj);

                    if (kj < 0)
                    {
                        StartCoroutine(Thing(1, kj));
                    }

                    if (kj >= -210 && kj <= -150)
                    {
                        Red.SetActive(true);
                        red.Play();
                    }
                    if (kj >= -310 && kj <= -211)
                    {
                        Orange.SetActive(true);
                        orange.Play();
                    }
                    if (kj >= -410 && kj <= -311)
                    {
                        Yellow.SetActive(true);
                        yellow.Play();
                    }
                    if (kj >= -510 && kj <= -411)
                    {
                        Green.SetActive(true);
                        green.Play();

                    }
                    if (kj >= -610 && kj <= -511)
                    {
                        Blue.SetActive(true);
                        blue.Play();

                    }
                    if (kj >= -710 && kj <= -611)
                    {
                        Indigo.SetActive(true);
                        indigo.Play();

                    }
                    if (kj >= -810 && kj <= -711)
                    {
                        Violet.SetActive(true);
                        violet.Play();

                    }


                }
                if (EligibleLines2.Contains(hit.collider.gameObject))
                {
                    electron2parent.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + .035f, hit.collider.gameObject.transform.position.z - 1);
                    CurrentLineNumber2 = initScreen.levels2.IndexOf(hit.collider.gameObject);
                    EligibleLines2.Clear();
                    EligibleLines1.Clear();
                    if (CurrentLineNumber2 == 0)
                    {
                        electron2.GetComponent<Animation>().Stop("wiggle 1");

                    }
                    else
                    {
                        electron2.GetComponent<Animation>().Play("wiggle 1");
                        electron2.GetComponent<Animation>()["wiggle 1"].speed = .25f + .15f * CurrentLineNumber2;
                    }
                    var kj = kJDic[hit.collider.gameObject];
                    UnityEngine.Debug.Log(kj);

                    if (kj >= -210 && kj <= -150)
                    {
                        Red.SetActive(true);
                        red.Play();
                    }
                    if (kj >= -310 && kj <= -211)
                    {
                        Orange.SetActive(true);
                        orange.Play();
                    }
                    if (kj >= -410 && kj <= -311)
                    {
                        Yellow.SetActive(true);
                        yellow.Play();

                    }
                    if (kj >= -510 && kj <= -411)
                    {
                        Green.SetActive(true);
                        green.Play();

                    }
                    if (kj >= -610 && kj <= -511)
                    {
                        Blue.SetActive(true);
                        blue.Play();

                    }
                    if (kj >= -710 && kj <= -611)
                    {
                        Indigo.SetActive(true);
                        indigo.Play();

                    }
                    if (kj >= -810 && kj <= -711)
                    {
                        Violet.SetActive(true);
                        violet.Play();

                    }

                }
                if (Red.activeSelf == true && Orange.activeSelf == true && Yellow.activeSelf == true && Green.activeSelf == true && Blue.activeSelf == true && Indigo.activeSelf == true && Violet.activeSelf == true)
                {
                    SceneManager.LoadScene(3);
                }
            }
        }
    }

    public void FlipFirstCard()
    {
        while (pog == -1)
        {
            pog = yuh();
        }
        var CardNumber = pog;
        foreach (GameObject card in FlippedCards)
        {
            var pos = card.transform.position;
            card.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
        }
        Transform newcard = Instantiate(Cards[CardNumber].transform, new Vector3(-6, 1, 0), new Quaternion(0, 0, 0, 0), this.transform);
        newcard.gameObject.GetComponentInChildren<Animation>().Play("yuhh");
        FlippedCards.Add(newcard.gameObject);
        LastCard = CardNumber;
        pog = -1;
    }

    public int yuh()
    {
        var CardNumber = Random.Range(0, 11);
        EligibleLines1 = CheckLines(initScreen.levels, initScreen.chosenElement, CardNumber, CurrentLineNumber1);
        EligibleLines2 = CheckLines(initScreen.levels2, initScreen.chosenElement2, CardNumber, CurrentLineNumber2);
        if ((EligibleLines1.Count() + EligibleLines2.Count()) < 2)
        {
            return -1;
        }
        if (CardNumber == LastCard)
        {
            return -1;
        }
        else
        {
            return CardNumber;
        }
    }

    public List<GameObject> CheckLines(List<GameObject> Linelist, Element element, int CardNumber, int CurrentLineNumber)
    {
        List<GameObject> ReturnLines = new List<GameObject>();

        foreach (GameObject line in Linelist)
        {
            if (line == Linelist[CurrentLineNumber])
            {
                continue;
            }

            int LineNumber = Linelist.IndexOf(line);
            var kJ2 = element.kJValues[LineNumber];
            var kJ1 = element.kJValues[CurrentLineNumber];
            var kJDiff = kJ2 - kJ1;
            switch (CardNumber)
            {
                case 0:
                    if (kJDiff <= 630 && kJDiff > 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 1:
                    if (LineNumber == 2)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 2:
                    if (LineNumber == 1)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 3:
                    if (kJDiff <= 700 && kJDiff >= 300)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 4:
                    if (kJDiff < 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 5:
                    if (LineNumber == 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 6:
                    if (kJDiff <= 300 && kJDiff >= 170)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 7:
                    if (kJDiff > 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 8:
                    if (CurrentLineNumber == 1)
                    {
                        if (LineNumber == 0)
                        {
                            ReturnLines.Add(line);
                            kJDic[line] = kJDiff;
                        }
                    }
                    else
                    {
                        if (LineNumber == (CurrentLineNumber - 2))
                        {
                            ReturnLines.Add(line);
                            kJDic[line] = kJDiff;
                        }
                    }
                    break;
                case 9:
                    if (kJDiff <= 700 && kJDiff >= 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                case 10:
                    if (kJDiff <= 800 && kJDiff >= 0)
                    {
                        ReturnLines.Add(line);
                        kJDic[line] = kJDiff;
                    }
                    break;
                default:
                    break;
            }
        }
        return ReturnLines;
    }

    IEnumerator Thing(int wellnum, int kj)
    {
        UnityEngine.Debug.Log("doing thing");
        if (wellnum == 1)
        {
            var pos = Camera.main.WorldToScreenPoint(electron1parent.transform.position);
            scoretext.gameObject.transform.position = new Vector3(pos.x + 20, pos.y, pos.z);
            scoretext.text = "" + kj;
            while(scoretext.color.a >= 0)
            {
                var size = scoretext.fontSize + 2;
                scoretext.fontSize = (int)size;
                var a = scoretext.color.a - .05f;
                scoretext.color = new Color(scoretext.color.r, scoretext.color.g, scoretext.color.b, a);
                yield return new WaitForSecondsRealtime(.01f);
            }
            UnityEngine.Debug.Log("did things");
        }
        
    }
}
