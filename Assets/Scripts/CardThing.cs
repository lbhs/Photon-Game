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
    public initializeScreen initScreen;
    public Dictionary<GameObject, int> kJDic;
    public int LastCard;
    public int pog;
    public Text scoretext;
    public List<GameObject> CompletedColors;
    public Ai ai;

    public List<aColor> colorss;
    public List<Well> wells;

    [System.Serializable] public class aColor
    {
        public GameObject ColorObject;
        public AudioSource ColorSound;
        public List<int> ColorBounds;
        public Color ActualColor;

        public aColor(GameObject col, AudioSource sou, List<int> bou, Color act)
        {
            ColorObject = col;
            ColorSound = sou;
            ColorBounds = bou;
            ActualColor = act;
        }
    }

    [System.Serializable] public class Well
    {
        public GameObject electron;
        public GameObject electronparent;
        public int CurrentLineNumber;
        public List<GameObject> EligibleLines;
        public List<GameObject> levellist;
        public Element element;
    }

    public void Start()
    {
        kJDic = new Dictionary<GameObject, int>();
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
                foreach (Well well in wells)
                {
                    if (well.EligibleLines.Contains(hit.collider.gameObject))
                    {
                        well.electronparent.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + .035f, hit.collider.gameObject.transform.position.z - 1);
                        well.CurrentLineNumber = well.levellist.IndexOf(hit.collider.gameObject);
                        foreach (Well wellz in wells)
                        {
                            wellz.EligibleLines.Clear();
                        }
                        if (well.CurrentLineNumber == 0)
                        {
                            well.electron.GetComponent<Animation>().Stop("wiggle");
                        }
                        else
                        {
                            well.electron.GetComponent<Animation>().Play("wiggle");
                            well.electron.GetComponent<Animation>()["wiggle"].speed = .25f + .15f * well.CurrentLineNumber;
                        }
                        var kj = kJDic[hit.collider.gameObject];
                        UnityEngine.Debug.Log(kj);

                        foreach (aColor col in colorss)
                        {
                            if (-kj > col.ColorBounds[0] && -kj < col.ColorBounds[1])
                            {
                                col.ColorObject.SetActive(true);
                                col.ColorSound.Play();
                                CompletedColors.Add(col.ColorObject);
                                StartCoroutine(Thing(well, kj, col.ActualColor));
                            }
                        }
                    }
                }
                if(CompletedColors.Count() == 9)
                {
                    SceneManager.LoadScene(3);
                }
            }
        }
    }

    public void FlipFirstCard()
    {
        wells[0].element = initScreen.chosenElement;
        wells[1].element = initScreen.chosenElement2;

        //      while (pog == -1)
        //       {
        //          pog = yuh();
        //     }
        //    var CardNumber = pog;
        var CardNumber = ai.PickACard();

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
        foreach (Well well in wells)
        {
            well.EligibleLines = CheckLines(well, CardNumber);
        }
        if ((wells[0].EligibleLines.Count() + wells[1].EligibleLines.Count()) < 2)
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

    public List<GameObject> CheckLines(Well well, int CardNumber)
    {
        List<GameObject> ReturnLines = new List<GameObject>();
        var Linelist = well.levellist;
        var element = well.element;
        var CurrentLineNumber = well.CurrentLineNumber;

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

    IEnumerator Thing(Well well, int kj, Color color)
    {
        scoretext.color = color;
        scoretext.fontSize = 14;

            var pos = Camera.main.WorldToScreenPoint(well.electronparent.transform.position);
            var randomyuh = Random.Range(0, 2);
            scoretext.text = "" + kj;
            if (randomyuh == 0)
            {
                scoretext.gameObject.transform.position = new Vector3(pos.x + 30, pos.y, pos.z);
                while (scoretext.color.a >= 0)
                {
                    var size = scoretext.fontSize + 1;
                    scoretext.fontSize = (int)size;
                    var a = scoretext.color.a - .0000001f;
                    scoretext.color = new Color(scoretext.color.r, scoretext.color.g, scoretext.color.b, a);
                    var newpos = scoretext.gameObject.transform.position + new Vector3(3, 2.5f, 0);
                    scoretext.gameObject.transform.position = newpos;
                    yield return new WaitForSecondsRealtime(.01f);
                }
            }
            if (randomyuh == 1)
            {
                scoretext.gameObject.transform.position = new Vector3(pos.x - 30, pos.y, pos.z);
                while (scoretext.color.a >= 0)
                {
                    var size = scoretext.fontSize + 1;
                    scoretext.fontSize = (int)size;
                    var a = scoretext.color.a - .000001f;
                    scoretext.color = new Color(scoretext.color.r, scoretext.color.g, scoretext.color.b, a);
                    var newpos = scoretext.gameObject.transform.position + new Vector3(-3, 2.5f, 0);
                    scoretext.gameObject.transform.position = newpos;
                    yield return new WaitForSecondsRealtime(.01f);
                }
            }
    }
}
