using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BinaryRiver : MonoBehaviour
{

    public Sprite closed;
    public Sprite opened;

    Image enveloppe;
    public int index;

    public float timer;
    public float triggerTimer;
    public float minTimer;
    public float maxTimer;

    public int nb;

    float animTimer;
    float desiredAnimTimer = .5f;

    int popping = 0;

    public bool isWaiting = false;
    public bool isReading = false;

    
    static private BinaryRiver _br;
    static public BinaryRiver br
    {
        get
        {
            return _br;
        }
    }

    void Awake()
    {
        _br = this;
    }

    void Start()
    {
        enveloppe = GetComponent<Image>();
        enveloppe.enabled = false;
        triggerTimer = UnityEngine.Random.Range(minTimer, maxTimer);
    }

    public void OpenMail()
    {
        isWaiting = false;
        isReading = true;
        enveloppe.sprite = opened;
        string knot = "EMPLOYEE_MESSAGE_" + index.ToString();
        index++;
        NarrativeUI.ui.SwitchActive(true);
        NarrativeUI.ui.ReadKnot(knot, NarrativeUI.ui._inkStory.TagsForContentAtPath(knot)[0]);
    }

    public void CloseMail()
    {
        isReading = false;
        animTimer = 0.0f;
        popping = -1;
        timer = 0f;
        triggerTimer = UnityEngine.Random.Range(minTimer, maxTimer);
    }

    void Pop()
    {
        enveloppe.enabled = true;
        enveloppe.sprite = closed;
        animTimer = 0.0f;
        popping = 1;
    }


	// Update is called once per frame
	void Update ()
    {

            if (Desk.d.phone.callsCounter > 0 && index < nb && !isReading && !isWaiting && !NarrativeUI.ui.STOPEVERYTHING && (GhuestManager.gm.currentGhuest == null || (GhuestManager.gm.currentGhuest != null && GhuestManager.gm.currentGhuest.name != "???")))
            {
                timer += Time.deltaTime;
                if (timer >= triggerTimer)
                {
                    timer = 0.0f;
                    Pop();
                }
            }

            if (popping == 1)
            {
                animTimer += Time.deltaTime;
                if (animTimer >= desiredAnimTimer)
                {
                    popping = 0;
                    isWaiting = true;
                    AudioManager.am.PlayMail();
                }
                enveloppe.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Utilities.Ease.bounceOut(animTimer / desiredAnimTimer));
                enveloppe.color = Color.Lerp(Color.black, Color.white, Utilities.Ease.backOut(animTimer / desiredAnimTimer));
            }
            else if (popping == -1)
            {
                animTimer += Time.deltaTime;
                if (animTimer >= desiredAnimTimer)
                {
                    popping = 0;
                    enveloppe.enabled = false;
                }
                enveloppe.rectTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, Utilities.Ease.bounceIn(animTimer / desiredAnimTimer));
                enveloppe.color = Color.Lerp(Color.white, Color.black, Utilities.Ease.backIn(animTimer / desiredAnimTimer));
            }
        
	}
}
