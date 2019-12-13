using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public List<AudioClip> basicDrag;
    public List<AudioClip> basicDrop;
    public List<AudioClip> drawerOpen;
    public List<AudioClip> drawerClose;
    public List<AudioClip> keysDrag;
    public List<AudioClip> keysDrop;

    public List<AudioClip> randomVoice;
    public List<AudioClip> cashbagsVoice;

    public List<AudioClip> mouseOver;
    public List<AudioClip> click;
    public List<AudioClip> clickMail;
    public List<AudioClip> writing;

    public List<AudioClip> badGuy;

    public AudioClip bell;
    public AudioClip skip;

    public AudioClip notification;
    public AudioClip radioOn;
    public AudioClip radioOff;

    static AudioManager _am;
    AudioSource audioSource;

    static public AudioManager am
    {
        get
        {
            return _am;
        }
    }

    private bool on = true;

    void Awake()
    {
        _am = this;
        
    }

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();

}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayRandom(List<AudioClip> list)
    {
        audioSource.PlayOneShot(list[Random.Range(0, list.Count)]);
    }

    public void PlayBell()
    {
        audioSource.PlayOneShot(bell);
    }

    public void PlayMail()
    {
        audioSource.PlayOneShot(notification);
    }

    public void PlaySkip()
    {
        audioSource.PlayOneShot(skip);
    }

    public void PlayVoice(string cName)
    {
        if(cName == "Cashbags Michael")
        {
            PlayRandom(cashbagsVoice);
        }
        else if(cName == "BADGUY")
        {
            PlayRandom(badGuy);
        }
        else
        {
            foreach(Ghuest g in GhuestManager.gm.customGuests)
            {
                if(g.name == cName && g.voice != null && g.voice.Count > 0)
                {
                    PlayRandom(g.voice);
                    return;
                }
            }
            PlayRandom(randomVoice);
        }
    }

    public void PlayRadio(bool v)
    {
        if (v)
            audioSource.PlayOneShot(radioOn);
        else
            audioSource.PlayOneShot(radioOff);
    }
}
