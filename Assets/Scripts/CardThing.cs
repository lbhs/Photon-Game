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
    public Text LosingEnergyText;
    public Text GainingEnergyText;
    public Button button;
    public List<GameObject> CompletedColors;
    public Ai ai;

    public List<aColor> colorss;
    public List<Well> wells;

    public AudioSource cardSound;

    public Text tText1;
    public Text tText2;
    public Text tText3;
    public Text tText4;
    public Text tText5;
    public Text tText6;
    public Button menu;

    public int tutorialCounter;

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
        if (selectElement.elementNames == "Hydrogen")
        {
            tText3.gameObject.SetActive(false);
            tText4.gameObject.SetActive(false);
            tText5.gameObject.SetActive(false);
            tText6.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);
        }
        
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
                        if (tutorialCounter == 2)
                        {
                            tText5.gameObject.SetActive(false);
                            tText6.gameObject.SetActive(true);
                            menu.gameObject.SetActive(true);
                        }
                        if (tutorialCounter == 1)
                        {
                            tText4.gameObject.SetActive(true);
                            tText2.gameObject.SetActive(true);
                            tText3.gameObject.SetActive(false);
                        }
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
                        if(kj > 0)
                        {
                            StartCoroutine(IncreaseEnergyAnimation(well, kj, hit.collider.gameObject));
                        }

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
        
        if (selectElement.elementNames == "Hydrogen")
        {
            if (tutorialCounter == 1)
            {
                tText2.gameObject.SetActive(false);
                tText4.gameObject.SetActive(false);
                tText5.gameObject.SetActive(true);
                tutorialCounter += 1;
            }
            else
            {
                tText1.gameObject.SetActive(false);
                tText2.gameObject.SetActive(false);
                tText3.gameObject.SetActive(true);
                tutorialCounter += 1;
            }
            
            

        }

        wells[0].element = initScreen.chosenElement;
        wells[1].element = initScreen.chosenElement2;

        var CardNumber = ai.PickACard();

        foreach (Well well in wells)
        {
            well.EligibleLines = CheckLines(well, CardNumber);
        }

        foreach (GameObject card in FlippedCards)
        {
            var pos = card.transform.position;
            card.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
        }
        Transform newcard = Instantiate(Cards[CardNumber].transform, new Vector3(-6, 1, 0), new Quaternion(0, 0, 0, 0), this.transform);
        newcard.gameObject.GetComponentInChildren<Animation>().Play("yuhh");
        FlippedCards.Add(newcard.gameObject);
        LastCard = CardNumber;
        cardSound.Play(); 
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
        LosingEnergyText.color = color;
        LosingEnergyText.fontSize = 5;

            var pos = Camera.main.WorldToScreenPoint(well.electronparent.transform.position);
            var randomyuh = Random.Range(0, 2);
            LosingEnergyText.text = "" + kj;
            if (randomyuh == 0)
            {
                LosingEnergyText.gameObject.transform.position = new Vector3(pos.x + 50, pos.y - 70, pos.z);
                while (LosingEnergyText.color.a >= 0)
                {
                        var size = LosingEnergyText.fontSize + 1;
                        LosingEnergyText.fontSize = (int)size;
                        var a = LosingEnergyText.color.a - .012f;
                        LosingEnergyText.color = new Color(LosingEnergyText.color.r, LosingEnergyText.color.g, LosingEnergyText.color.b, a);
                        var newpos = LosingEnergyText.gameObject.transform.position + new Vector3(1.3f, 1f, 0);
                        LosingEnergyText.gameObject.transform.position = newpos;
                        yield return new WaitForSecondsRealtime(.01f);
                }
            }
            if (randomyuh == 1)
            {
                LosingEnergyText.gameObject.transform.position = new Vector3(pos.x - 50, pos.y - 70, pos.z);
                while (LosingEnergyText.color.a >= 0)
                {
                        var size = LosingEnergyText.fontSize + 1;
                        LosingEnergyText.fontSize = (int)size;
                        var a = LosingEnergyText.color.a - .012f;
                        LosingEnergyText.color = new Color(LosingEnergyText.color.r, LosingEnergyText.color.g, LosingEnergyText.color.b, a);
                        var newpos = LosingEnergyText.gameObject.transform.position + new Vector3(-1.3f, 1f, 0);
                        LosingEnergyText.gameObject.transform.position = newpos;
                        yield return new WaitForSecondsRealtime(.01f);
                }
            }
        LosingEnergyText.gameObject.transform.position = new Vector3(672.2f, -14.216f, 0);
    }

    IEnumerator IncreaseEnergyAnimation(Well well, int kj, GameObject line)
    {
        GainingEnergyText.color = new Color(1, 1, 1, 1);
        GainingEnergyText.fontSize = 40;
        GainingEnergyText.text = "+" + kj;
        Vector3 start = button.gameObject.transform.position + new Vector3(250, 0, 0);
        float timepassed = 0;

        while (timepassed < 1f)
        {
            float t = timepassed / 1f;
            float t2 = t * t;
            float t3 = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
            var x = Mathf.Lerp(start.x, cam.WorldToScreenPoint(line.transform.position).x + 50, t);
            var y = Mathf.Lerp(start.y, cam.WorldToScreenPoint(line.transform.position).y - 40, t2);
            GainingEnergyText.gameObject.transform.position = new Vector3(x, y, 0);
            GainingEnergyText.fontSize = (int)Mathf.Lerp(30, 10, t);
            GainingEnergyText.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, .2f), t3);
            timepassed += Time.deltaTime;

            yield return null;
        }

        GainingEnergyText.color = new Color(1, 1, 1, 0);
    }
}
