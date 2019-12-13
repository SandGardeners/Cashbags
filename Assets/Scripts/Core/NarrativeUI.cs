using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System;
using Utilities;

public class NarrativeUI : MonoBehaviour
{

    static private NarrativeUI _ui;
    static public NarrativeUI ui
    {
        get
        {
            return _ui;
        }
    }


    //State machine
    public bool waitingForInput = false;
    bool waitingForContinue = false;
    bool displayChoices;
    //String character per character
    bool finishedString;
    string currentString = "";
    int stringIndex = 0;
    float characterTimer = 0.0f;
    float characterDelay = 0.0f;
    //Knots switching
    string currentKnot;
    string desiredKnot = "";

    //Story
    public Story _inkStory;

    //EDITOR READABLE

    public Color desiredHiglightColor;

    //GameObjects reference for lazy peoples
    public GameObject readerContainer;

    public Text currentTextObject;
    public bool verbose;
    //delay between letters
    public float wantedDelay;

    //Story txt 
    public TextAsset storyJSON;

    public GameObject effectPanel = null;
    public Image fonduPanel = null;
    private GameObject activablesGo = null;
    float transiTimer = -1.0f;
    public float desiredTransi = 1.0f;
    private bool characterSpeaking = false;

    [SerializeField]
    public UISettings settings;

    private List<string> waitingTags;

    public bool uiActive;

    public Text talkingCharacter;
    public int sfxLetterDelay = 3;

   // string cName = "Cashbags Michael";
    string ghuestName = "";
    bool isGhuestSpeaking;
    /*
    public AudioClip textBoxFeedback;
    public List<AudioClip> textBoxFeedbackChet;
    public AudioClip musicIntro;
    public AudioClip musicMall;
    public AudioClip musicCouloir;
    public AudioClip musicGramophone;


    private AudioClip transitionClip = null;
    private float transiMusicTimer = 0.0f;

    */

    private List<string> currentChoices;
    public GameObject LinesContainer;
    private bool fastForward;

    public bool STOPEVERYTHING = false;
    public outro endGame;
    public static List<GhuestEvent> ParseAvailableEventsForGhuest(string knot)
    {
        List<GhuestEvent> list = new List<GhuestEvent>();
        if(_ui != null)
        {
            foreach(string s in _ui._inkStory.TagsForContentAtPath(knot))
            {
                if (s == GhuestEvent.CHECK_IN.ToString())
                    list.Add(GhuestEvent.CHECK_IN);
                if (s == GhuestEvent.PHONECALL.ToString())
                    list.Add(GhuestEvent.PHONECALL);
                if (s == GhuestEvent.MAIL.ToString())
                    list.Add(GhuestEvent.MAIL);
                if (s == GhuestEvent.CHECK_OUT.ToString())
                    list.Add(GhuestEvent.CHECK_OUT);
            }
        }
        return list;
    }

    void Awake()
    {
        _ui = this;
        _inkStory = new Story(storyJSON.text);
    }

    protected void Start()
    {
      

        _inkStory.BindExternalFunction("ResetStory", () =>
        {
            ResetInk();
        });

        _inkStory.BindExternalFunction("EndEvent", () =>
        {
            GhuestManager.gm.EndEvent();
        });

        _inkStory.BindExternalFunction("HangUp", () =>
        {
            Desk.d.phone.HangUp();
        });
        _inkStory.BindExternalFunction("EndEmployeeMessage", () =>
        {
            BinaryRiver.br.CloseMail();
        });
        _inkStory.BindExternalFunction("GiveItem", (string n) =>
        {
            Desk.d.drawers.ShowItem(n);
        });
        _inkStory.BindExternalFunction("TriggerGameFinale", () =>
        {
            FINALE();
        });

        _inkStory.BindExternalFunction("ActualFinish", () =>
        {
            endGame.finishThatShit();
        });


        currentTextObject = readerContainer.transform.GetChild(0).GetComponent<Text>();
        currentChoices = new List<String>();
        waitingTags = new List<String>();
        waitingForInput = false;
        waitingForContinue = false;
        SwitchActive(false);
    }

    public void FINALE()
    {
        STOPEVERYTHING = true;
        endGame.Trigger();
    }

    public void ResetInk()
    {
        SwitchActive(false);
    }

    public void ReadKnot(string knot, string _ghuestName)
    {
        ghuestName = _ghuestName;
        desiredKnot = "";
        List<string> tags = _inkStory.TagsForContentAtPath(knot);
        if (tags != null)
        {
            //      TagTrigger(tags, knot);
        }

        currentKnot = knot;
        _inkStory.ChoosePathString(currentKnot);
        if (!_inkStory.canContinue)
        {
            // Debug.Break();
        }
        else
        {

            // Debug.Break();
        }
    }

    private void ChoiceInput(int c)
    {
        displayChoices = false;
        AcceptMyChoice(c);
    }

    public void PleaseProceed()
    {
        if (waitingForContinue)
        {
            if (waitingTags.Count != 0)
            {
                TagTrigger(waitingTags, currentKnot);
                waitingTags.Clear();
            }
            waitingForContinue = false;
            ClearScreen();
        }
    }

    public void AcceptMyChoice(int i)
    {
        if (i < _inkStory.currentChoices.Count)
        {
            ClearScreen();
            _inkStory.ChooseChoiceIndex(i);

            waitingForInput = false;
            waitingForContinue = false;
        }
    }

    void Update()
    {
        if (uiActive)
        {
            UpdateQuandJePasseDuneLigneALautre();
            if (!finishedString)
                UpdateQuandJaiUneLigneALire();
            else if (currentChoices.Count > 0)
            {
                currentString = currentChoices[0];
                currentChoices.Remove(currentChoices[0]);
                finishedString = false;
                stringIndex = 0;
                int choiceObjectIndex = readerContainer.transform.childCount - 2;
                GameObject r = Instantiate(readerContainer.transform.GetChild(0).gameObject, readerContainer.transform) as GameObject;// new GameObject("choiceLine", typeof(LayoutElement));
                ClickableChoice cc = r.AddComponent<ClickableChoice>();
                cc.index = choiceObjectIndex;
                //  cc.enabled = false;
                r.transform.SetParent(readerContainer.transform, false);
                currentTextObject = r.GetComponent<Text>();
                currentTextObject.text = "";
            }
            else
            {
                UpdateQuandJattendUnInput();
            }

            if (transiTimer != -1.0f)
            {
                transiTimer -= Time.deltaTime;

                float alpha = transiTimer >= desiredTransi / 2.0f ? Utilities.Ease.cubeOut(Mathf.Lerp(0, 1, Mathf.InverseLerp(desiredTransi, desiredTransi / 2.0f, transiTimer))) : Ease.cubeOut(Mathf.Lerp(1, 0, Mathf.InverseLerp(desiredTransi / 2.0f, 0.0f, transiTimer)));
                //    fonduPanel.color = new Color(0, 0, 0, alpha);

                if (transiTimer <= desiredTransi / 2.0f && desiredKnot != "")
                    EndTransition();

                if (transiTimer < 0.0f)
                    transiTimer = -1.0f;
            }
        }
        /*
        if(transiMusicTimer > 0.0f)
        {
            float volume = transiMusicTimer >= 1.5f ? Utilities.Ease.cubeOut(Mathf.Lerp(1, 0, Mathf.InverseLerp(3.0f, 1.5f, transiMusicTimer))) : Ease.cubeOut(Mathf.Lerp(0, 1, Mathf.InverseLerp(1.5f, 0.0f, transiMusicTimer)));
           // GameManager.gm.audioSource.volume = volume;

            if (transiMusicTimer <= 1.5f && transitionClip != null)
            {
             //   GameManager.gm.audioSource.clip = transitionClip;
              //  GameManager.gm.audioSource.Play();
                transitionClip = null;
            }

            transiMusicTimer -= Time.deltaTime;
            if(transiMusicTimer < 0.0f)
            {
                transiMusicTimer = 0.0f;
            }
        }
        */
    }

    public void SwitchActive(bool _uiActive)
    {
        uiActive = _uiActive;

        if (!STOPEVERYTHING && !Desk.d.radioFade)
        {
            if (uiActive)
            {
                if (PictureShower.ps.target != null)
                    PictureShower.ps.Close();
                if(!Desk.d.muteRadio)
                    Desk.d.radio.volume = NarrativeUI.ui.settings.volumeRadio / 2;
            }
            else
            {
                if(!Desk.d.muteRadio)
                    Desk.d.radio.volume = NarrativeUI.ui.settings.volumeRadio;
            }
        }
        foreach (Transform t in transform)
        {

            t.gameObject.SetActive(uiActive);
        }
        Desk.d.Switch(!uiActive);
    }
public void OnEnable()
    {
    }

    public void OnDisable()
    {
    }

    void UpdateQuandJePasseDuneLigneALautre()
    {
        if (!waitingForInput)
        {
            if (_inkStory.canContinue)
            {
                if (!waitingForContinue)
                {
                    string s;
                    if (fastForward)
                        s = _inkStory.ContinueMaximally();
                    else
                        s = _inkStory.Continue();

                    if (s != "")
                    {
                        bool isGhuest = false;
                        List<string> tags = _inkStory.currentTags;
                        if(tags.Count > 0)
                        {
                            foreach(string tag in tags)
                            {
                                if (tag == "ghuest")
                                {
                                    isGhuest = true;
                                }
                            }
                        }
                        FeedLine(s, false, isGhuest);

                        /*
                        if (tags.Count > 0)
                        {
                            List<string> wList = new List<string>();
                            for (int i = 0; i < tags.Count; i++)
                            {
                                if (i < tags.Count - 1 && tags[i] == "immediate")
                                {
                                    List<string> rList = new List<string>();
                                    rList.Add(tags[i + 1]);
                                    TagTrigger(rList, currentKnot);
                                }
                                else if (i == 0 || tags[i - 1] != "immediate")
                                {
                                    wList.Add(tags[i]);
                                }
                            }
                            waitingTags = wList;
                        }*/


                        waitingForContinue = true;
                    }
                }
            }
            else if (_inkStory.currentChoices.Count > 0)
            {
                waitingForContinue = false;

                for (int i = 0; i < _inkStory.currentChoices.Count; ++i)
                {
                    Choice choice = _inkStory.currentChoices[i];
                    string s = "\t" + choice.text;
                    
                    FeedLine(s, true, false);
                    waitingForInput = true;
                }
            }

            else if (!waitingForContinue)
            {
                if (waitingTags.Count > 0)
                    TagTrigger(waitingTags, currentKnot);

                if (transiTimer == -1.0f)
                {
                    transiTimer = 0.0f;


                    if (desiredKnot != "")
                    {
                        StartTransition(desiredTransi);
                    }
                    else
                        SwitchActive(false);
                }
            }
        }
    }

    private void UpdateQuandJaiUneLigneALire()
    {
        if (stringIndex < currentString.Length)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (characterDelay > 0.0f)
                {
                    AudioManager.am.PlaySkip();
                    characterDelay = 0.0f;
                }
                else
                {
                    currentTextObject.text = currentString;
                    stringIndex = currentString.Length;
                    characterSpeaking = false;
                }
            }

            else if (characterTimer >= characterDelay)
            {
                char c = currentString[stringIndex++];
                if (c.Equals('"') && !currentString.Substring(0, stringIndex - 1).Contains("Choice"))
                {
                    characterSpeaking = !characterSpeaking;
                }
                else if (characterDelay > 0.0f)
                {
                    if (c.Equals('.'))
                    {
                        characterDelay = wantedDelay * 10.0f;
                    }
                    else if (c.Equals(','))
                    {
                        characterDelay = wantedDelay * 5.0f;
                    }
                    else
                    {
                        characterDelay = wantedDelay;
                    }
                }

                currentTextObject.text += c;


                if ((stringIndex % sfxLetterDelay == 0 || c.Equals('.')) && characterDelay != 0.0f)
                {
                    if (isGhuestSpeaking)
                    {
                        if (STOPEVERYTHING)
                            AudioManager.am.PlayVoice("BADGUY");
                        else
                            AudioManager.am.PlayVoice(ghuestName);
                    }
                    else
                        AudioManager.am.PlayVoice("Cashbags Michael");
                }
                    
                characterTimer = 0.0f;
            }
            else characterTimer += Time.deltaTime;

        }
        else
        {
            finishedString = true;
        }

    }

    private void UpdateQuandJattendUnInput()
    {
        if (waitingForContinue)
        {
            if (Input.GetKeyDown(KeyCode.Space) || fastForward || Input.GetMouseButtonDown(0)) PleaseProceed();
        }

        if (waitingForInput)
        {
            if (fastForward) ChoiceInput(0);
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (Input.GetKeyDown(i + KeyCode.Keypad1) || Input.GetKeyDown(i + KeyCode.Alpha1))
                    {
                        ChoiceInput(i);
                    }
                }
            }
        }

    }

    public void HoverChoice(Text targetText)
    {
        targetText.text += "<";
    }

    private void StartTransition(float desiredTransi)
    {
        transiTimer = desiredTransi;
    }

    private void EndTransition()
    {
        ReadKnot(desiredKnot, ghuestName);
    }

    private void TagTrigger(List<string> currentTags, string knot)
    {
        if (currentTags != null)
        {
            foreach (string s in currentTags)
            {
            }
        }
    }

    public void FeedLine(string line, bool isChoice, bool isGhuest)
    {
        isGhuestSpeaking = isGhuest;
        if (verbose) Debug.Log(line + " - " + isChoice);
        characterSpeaking = false;
        if (!isChoice)
        {
            if (isGhuest)
                talkingCharacter.text = ghuestName;
            else
                talkingCharacter.text = "Cashbags Michael";
            currentString += line;
        }
        else
            currentChoices.Add(line);
        stringIndex = 0;
        characterTimer = 0.0f;
        characterDelay = wantedDelay;
        finishedString = false;
    }

    public void ClearScreen()
    {
        currentChoices.Clear();
        for (int i = 1; i < readerContainer.transform.childCount; i++)
        {
            if(readerContainer.transform.GetChild(i).gameObject.name != "CharacterName")
                Destroy(readerContainer.transform.GetChild(i).gameObject);
        }
        currentTextObject = readerContainer.transform.GetChild(0).GetComponent<Text>();
        currentTextObject.text = "";
        currentString = "";
    }
}
