 using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public enum CheckInStep
{
    WRITE_NAME,
    GET_MONEY,
    GIVE_KEY,
    BYE,
}

public enum CheckOutStep
{

}



public class GhuestManager : MonoBehaviour
{
    static private GhuestManager _gm;

    static List<string> availableNames;
    public List<Color> availableColors;

    public List<Ghuest> customGuests;
    public List<Ghuest> checkedGuests;

    List<string> availablePhonecallNames;
    Dictionary<string, string> MapKnotName;
    public TextAsset namesList;
    public TextAsset colorsList;

    public Ghuest currentGhuest = null;
    public GhuestEvent currentEvent = GhuestEvent.NONE;

    public CheckInStep checkInStep = CheckInStep.WRITE_NAME;

    public Sprite headRandom;

    public Image ghuestHeadHolder;

    public string lastName = "";

    public int ghuestCount = 0;
    public float timer = 0.0f;
    public float triggerTimer = 0.0f;

    int randomCalls = 0;

    public float minTimer = 5.0f;
    public float maxTimer = 30.0f;


    static public GhuestManager gm
    {
        get
        {
            return _gm;
        }
    }

    public void Awake()
    {
        _gm = this;
    }

    public bool ghuestHasEvent(Ghuest g, GhuestEvent e)
    {
        foreach(GhuestEvent _e in g.events)
        {
            if (_e == e)
                return true;
        }
        return false;
    }

    public string getRandomName(bool isForCustom)
    {
        string n;

        int index = UnityEngine.Random.Range(0, availableNames.Count);
        n = availableNames[index];
        if (availableNames.Count > 1)
            availableNames.Remove(availableNames[index]);

        if (!isForCustom)
            availablePhonecallNames.Add(n);
        return n;
    }

    public void PopGhuest(Ghuest g = null)
    {
        if (g != null)
        {
            currentGhuest = g;
        }
        else
        {
            currentGhuest = new Ghuest(getRandomName(false), "RANDOM_GUEST");
        }

        if(currentGhuest.mainKnot == "STORY_GUEST_2")
        {
            Desk.d.StartFadingRadio();
        }


        ghuestHeadHolder.gameObject.SetActive(true);

        if (currentGhuest.headSprite == null)
        {
            ghuestHeadHolder.sprite = headRandom;
            ghuestHeadHolder.color = availableColors[UnityEngine.Random.Range(0, availableColors.Count)];
        }
        else
        {
            ghuestHeadHolder.sprite = currentGhuest.headSprite;
            ghuestHeadHolder.color = Color.white;
        }

        AudioManager.am.PlayBell();
        timer = 0.0f;
        ghuestCount++;
        //currentGhuest.events.Remove(currentGhuest.events[0]);
        lastName = currentGhuest.name;
        NarrativeUI.ui.SwitchActive(true);
        if (currentGhuest.events != null && currentGhuest.events.Count != 0)
        {
            currentEvent = currentGhuest.events[0];
            NarrativeUI.ui.ReadKnot(currentGhuest.mainKnot + "." + currentEvent.ToString(), currentGhuest.name);
        }
        else
        {
            currentEvent = GhuestEvent.SPECIAL;
            NarrativeUI.ui.ReadKnot(currentGhuest.mainKnot, currentGhuest.name);
        }
    }

    public void Phonecall(int callIndex)
    {
        string specialKnot = "RANDOM_GUEST";
        string n = "";

        if (checkedGuests.Count > 0 && (callIndex == 0 || randomCalls == 2 || UnityEngine.Random.value <= 0.5f))
        {
            n = checkedGuests[0].name;
            specialKnot = checkedGuests[0].mainKnot;
            checkedGuests.Remove(checkedGuests[0]);
            randomCalls = 0;
        }
        else
        {
            randomCalls++;
        }

        if(specialKnot == "RANDOM_GUEST")
        {
            n= availablePhonecallNames[UnityEngine.Random.Range(0, availablePhonecallNames.Count)];
        }

        NarrativeUI.ui.SwitchActive(true);
        NarrativeUI.ui.ReadKnot(specialKnot+".PHONECALL", n);
    }

    public void EndEvent()
    {
        timer = 0.0f;
        triggerTimer = UnityEngine.Random.Range(minTimer, maxTimer);
        if(ghuestHasEvent(currentGhuest, GhuestEvent.PHONECALL) && currentGhuest.mainKnot != "RANDOM_GUEST")
            checkedGuests.Add(currentGhuest);
        currentGhuest = null;
        ghuestHeadHolder.gameObject.SetActive(false);
        currentEvent = GhuestEvent.NONE;
        checkInStep = CheckInStep.WRITE_NAME;
        NarrativeUI.ui._inkStory.variablesState["checkInStep"] = (int)checkInStep;
    }

    public void ProceedCheckIn()
    {
        checkInStep++;
        lastName = currentGhuest.name;
        NarrativeUI.ui._inkStory.variablesState["checkInStep"] = (int)checkInStep;
        switch (checkInStep)
        {
            case CheckInStep.WRITE_NAME:
                break;
            case CheckInStep.GET_MONEY:
                Desk.d.exchange.FeedObject(Instantiate(ExchangeZone.moneyObject));
                break;
            case CheckInStep.GIVE_KEY:
                break;
            case CheckInStep.BYE:
                Destroy(Desk.d.exchange.transform.GetChild(0).gameObject);
                break;
            default:
                break;
        }
        NarrativeUI.ui.SwitchActive(true);
        NarrativeUI.ui.ReadKnot(currentGhuest.mainKnot + "." + currentEvent.ToString(), currentGhuest.name);
 
    }

   
// Use this for initialization
    void Start()
    {
        string[] lines = namesList.text.Split('\n');
        availableNames = new List<string>(lines);
        checkedGuests = new List<Ghuest>();
        string[] colors = colorsList.text.Split('-');
        List<string> colorStrings = new List<string>(colors);
        foreach(string s in colorStrings)
        {
            Color c = Color.white;
            ColorUtility.TryParseHtmlString(s, out c);
            availableColors.Add(c);
        }
        int tmpIndexCheck = 0;

        foreach (Ghuest ch in customGuests)
        {
            ch.events = NarrativeUI.ParseAvailableEventsForGhuest(ch.mainKnot);
            if (ch.name == String.Empty)
                ch.name = getRandomName(true);

            ch.indexCheckin = tmpIndexCheck;
            tmpIndexCheck += UnityEngine.Random.Range(2, 4);
        }

        availablePhonecallNames = new List<string>();
        MapKnotName = new Dictionary<string, string>();
        triggerTimer = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEvent == GhuestEvent.NONE && !Desk.d.phone.isCalling && !BinaryRiver.br.isReading && !NarrativeUI.ui.STOPEVERYTHING)
        {
            timer += Time.deltaTime;
            if(timer >= triggerTimer)
            {

                Ghuest g = null;
                if(customGuests != null)
                {
                    foreach(Ghuest ch in customGuests)
                    {
                        if(ch.indexCheckin == ghuestCount)
                        {
                            g = ch;
                            break;
                        }
                    }
                }

                PopGhuest(g);
            }
        }
    }
}
