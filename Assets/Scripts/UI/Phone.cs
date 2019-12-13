using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour {

    public bool isRinging = false;
    public bool isCalling = false;
    public float timer = 0.0f;
    public float triggerTimer = 0.0f;
    public float minTimer = 25f;
    public float maxTimer = 45f;
    AudioSource source;

    float rotation = 0f;

    public int callsCounter = 0;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        ResetTrigger();
        timer = 0.0f;
	}

    void ResetTrigger()
    {
        timer = 0.0f;
        triggerTimer = Random.Range(minTimer, maxTimer);
    }

    void Ring()
    {
        isRinging = true;
        source.Play();
    }

    public void PickUp()
    {
        isRinging = false;
        isCalling = true;
        rotation = 0f;
        source.Stop();
        string knot;
  
        GhuestManager.gm.Phonecall(callsCounter++);
    }

    public void HangUp()
    {
        isCalling = false;
        ResetTrigger();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(!isRinging && !isCalling && GhuestManager.gm.ghuestCount >= 2 && !NarrativeUI.ui.STOPEVERYTHING && (GhuestManager.gm.currentGhuest == null || (GhuestManager.gm.currentGhuest != null && GhuestManager.gm.currentGhuest.name != "???")))
        {
            timer += Time.deltaTime;
            if(timer >= triggerTimer)
            {
                Ring();
            }
        }
        else if(isRinging)
        {
            rotation = Random.Range(-10f, 10f);
        }

        transform.GetChild(0).GetComponent<Image>().rectTransform.eulerAngles = Vector3.back * rotation;
	}
}
