using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardThing : MonoBehaviour
{
    /* This script is the main script that makes the game work. There are 3 main functions: Update which 
     * checks to see if a line has been clicked; FlipFirstCard() which flips the first card on top of the deck 
     * (it actually just creates a new card it doesnt flip the top one...); and CheckLines() which calculates all
     * the possible lines the player can go to for that specific card. There are also 2 animation coroutines 
     * at the end of the script. */

    public Camera cam;

    /* A list of all of the cards, in order, so that they can be referenced by index number. */
    public List<GameObject> Cards;

    /* A list of cards that have already been flipped. */
    public List<GameObject> FlippedCards;

    /* The object the initializeScreen script is on. */
    public initializeScreen initScreen;

    /* A dictionary storing the kJ value of each possible lines in the CheckLines() 
     * function so that the kJ values are easily accessible later . */
    public Dictionary<GameObject, int> kJDic;

    /* An int number for the last card so as to not have any repeats */
    public int LastCard;

    /* The texts that show the kJ value entering/leaving the well in the animations. */
    public Text LosingEnergyText;
    public Text GainingEnergyText;

    /* The button to flip the cards, which is on an invisible button above the deck of cards */
    public Button button;

    /* A list of the colors that have been completed for keeping track of when the game ends. */
    public List<GameObject> CompletedColors;

    /* The AI script*/
    public Ai ai;

    /* A list of all the wells in the scene and all the possible colors, both of which are classes defined below. 
     * This is designed so that more wells or colors can be added or the existing ones can be easily editable. */
    public List<aColor> colorss;
    public List<Well> wells;

    /* The sound for when the card flips*/
    public AudioSource cardSound;

    /* The text objects for the tutorial and an int to indicate which step of the turtorial the player is on */
    public Text tText1;
    public Text tText2;
    public Text tText3;
    public Text tText4;
    public Text tText5;
    public Text tText6;
    public Button menu;
    public int tutorialCounter;

    /* A color class that defines the ColorObject, which is the actual gameobject that appears in the top left when 
     * a player completes a color, the ColorSound, which is the audioclip for that color, the ColorBounds, which is 
     * a list of the upper and lower kj bounds for that color with lower first, and the ActualColor, which is the 
     * actual color value. It also has a constructor but I never used it. */
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

    /* A well class with the electron for that well, the parent object of the electron, an int for the 
     * CurrentLineNumber, which should only be edited by the script, a list of EligibleLines, which should only 
     * be assigned by the CheckLines() function, and a levellist that is all the lines in the well and that should
     * be assigned when creating a new well*/
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

        /* Checks if we are in the tutorial screen by checking if the current element is hydrogen. 
         * If we are in the tutorial, it sets up some of the text boxes. */
        if (selectElement.elementNames == "Hydrogen")
        {
            tText3.gameObject.SetActive(false);
            tText4.gameObject.SetActive(false);
            tText5.gameObject.SetActive(false);
            tText6.gameObject.SetActive(false);
            menu.gameObject.SetActive(false);
        }
    }

    /* This function constantly checks to see if the player has clicked on one of the lines that they 
     * are allowed to go to, determined by the EligibleLines list created in the CheckLines() function. 
     * If the player has clicked on one of those lines, it moves the electron there, updates the 
     * current line variable, etc.. */
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /* Creates a raycast. */
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                /* Checks if the object hit is one of the EligibleLines determined in CheckLines(). */
                foreach (Well well in wells)
                {
                    if (well.EligibleLines.Contains(hit.collider.gameObject))
                    {
                        /* This is more tutorial stuff that isn't important to the main game. */
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

                        /* Moves the electron parent, because the electron itself is animated, the parent object needs 
                         * to be moved when trying to change the position of the actual electron so as not to mess up the animation. 
                         * Then it updates the wells CurrentLineNumber. */
                        well.electronparent.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + .035f, hit.collider.gameObject.transform.position.z - 1);
                        well.CurrentLineNumber = well.levellist.IndexOf(hit.collider.gameObject);

                        /* Clears the list of EligibleLines so the player can't move more than once for one card. */
                        foreach (Well wellz in wells)
                        {
                            wellz.EligibleLines.Clear();
                        }

                        /* This starts/stops the animation of the electron wiggling. If the electron is on the ground level, 
                         * it doesn't wiggle because it has no energy. Otherwise it wiggles faster the farther up the well it is. */
                        if (well.CurrentLineNumber == 0)
                        {
                            well.electron.GetComponent<Animation>().Stop("wiggle");
                        }
                        else
                        {
                            well.electron.GetComponent<Animation>().Play("wiggle");
                            well.electron.GetComponent<Animation>()["wiggle"].speed = .25f + .15f * well.CurrentLineNumber;
                        }

                        /* Gets the kJ differnce that is created by moving to this line, which is already assigned in the dictionary by the CheckLines() function. */
                        var kj = kJDic[hit.collider.gameObject];

                        /* If the electron is moving up, it starts the animation of the energy going into the well. */
                        if (kj > 0)
                        {
                            StartCoroutine(IncreaseEnergyAnimation(well, kj, hit.collider.gameObject));
                        }

                        /* This iterates through all the colors and checks to see if the move to the new line is within the bounds of the color. If so, it activates 
                         * the color object, plays the sound, adds the color to the list of completed ones, and starts the animation showing the energy leaving the well. */
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

                /* Checks to see if the game is over and if so it loads the end scene. */
                if (CompletedColors.Count() == 9)
                {
                    SceneManager.LoadScene(3);
                }
            }
        }
    }

    /* This function is called when the player clicks on the button to flip a card. */
    public void FlipFirstCard()
    {
        /* More tutorial stuff... */
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

        /* Assigns the element of each well, this would be in the Start() function but if doesn't work in there. */
        wells[0].element = initScreen.chosenElement;
        wells[1].element = initScreen.chosenElement2;

        /* Creates the variable CardNumber, which is used to refer to the selected card by its index in the Cards list. */
        var CardNumber = -1;

        /* Checks if we are in the turtorial. If not, it calls the function PickACard() in the AI script. The AI does its 
         * thing and then returns which card it has picked, which is assigned to CardNumber. If we are in turotial a card
         * is assigned based on what step in the tutorial the player is on. */
        if (selectElement.elementNames != "Hydrogen")
        {
            CardNumber = ai.PickACard();
        }
        else
        {
            if (tutorialCounter == 2)
            {
                CardNumber = 4;
            }
            if (tutorialCounter == 3)
            {
                return;
            }
        }

        /* Calls the CheckLines() function to determine what the EligibleLines are based on the selected card. */
        foreach (Well well in wells)
        {
            well.EligibleLines = CheckLines(well, CardNumber);
        }

        /* Moves all the flipped cards back in the actual gamespace so that the new one is clearly visible
         * on top and they don't interfere with each other. */
        foreach (GameObject card in FlippedCards)
        {
            var pos = card.transform.position;
            card.transform.position = new Vector3(pos.x, pos.y, pos.z + 1);
        }

        /* Creates the new card and flips it. */
        Transform newcard = Instantiate(Cards[CardNumber].transform, new Vector3(-6, 1, 0), new Quaternion(0, 0, 0, 0), this.transform);
        newcard.gameObject.GetComponentInChildren<Animation>().Play("yuhh");
        FlippedCards.Add(newcard.gameObject);
        LastCard = CardNumber;
        cardSound.Play(); 
    }

    /* This script calculates all the lines that it is possible for the user to move too with a specific card and well. It returns those lines in a list. */
    public List<GameObject> CheckLines(Well well, int CardNumber)
    {
        /* Defines a list of lines to return. */
        List<GameObject> ReturnLines = new List<GameObject>();

        var Linelist = well.levellist;
        var element = well.element;
        var CurrentLineNumber = well.CurrentLineNumber;

        foreach (GameObject line in Linelist)
        {
            /* If the line is the current line it just skips it. Don't want players to move to the line they are already on. */
            if (line == Linelist[CurrentLineNumber])
            {
                continue;
            }

            int LineNumber = Linelist.IndexOf(line);

            /* Calculates the kJ diff between the line and the current line. */
            var kJ2 = element.kJValues[LineNumber];
            var kJ1 = element.kJValues[CurrentLineNumber];
            var kJDiff = kJ2 - kJ1;

            /* This switch statement switches between each of the cards. The properties of the cards are programmed manually here, 
             * so if new cards are created or edited then it needs to be changed here. This is an inefficient way of doing it,
             * perhaps it would be better to create a class for the cards. */
            switch (CardNumber)
            {
                /* Switches based on the card, if the line satisfies the requirements of the card, it adds that line to the list to return.
                 * It also assigns the kJ value to the line in the dictionary. */
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

    /* This coroutine creates the animation of the energy leaving the well and fading out. */
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

    /* This coroutine makes the animation of the energy going into the well from the cards. Definetly needs some improvement. */
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
