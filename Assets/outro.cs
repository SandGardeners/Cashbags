using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class outro : MonoBehaviour
{
    float timer = -5.0f;
    bool triggerFinale = false;
    bool triggered = false;
    float timerFinale = -3.0f;
    public Image fondu;
    public Image bg;
    public Image fg;
    public Image cashbags;
    public AudioSource source;
    public void Trigger()
    {
        gameObject.SetActive(true);
        Desk.d.muteRadio = true;
        Desk.d.radio.volume = 0.0f;
        triggered = true;
        source.Play();
        source.volume = 0;
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    public void finishThatShit()
    {
        triggerFinale = true;
    }

    void Update()
    {


        if(triggered)
        {
            timer += Time.deltaTime;
            if(source.volume <= .25f)
            {
                source.volume = Mathf.Lerp(0.0f, .25f, Mathf.InverseLerp(-3.0f, 2.0f, timer));
            }


            if(timer >= 0f)
            {
                fondu.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, Utilities.Ease.backInOut(timer / 2.0f));
                if(timer >= 2.0f)
                {
                    bg.enabled = true;
                    fg.enabled = true;
                    fondu.color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), (timer - 2.0f) / 4.0f);
                    if(timer >= 4.0f)
                    {
                        if (fondu.enabled == true)
                        {
                            fondu.enabled = false;
                        }
                        if(timer >= 6.0f)
                        {
                            NarrativeUI.ui.SwitchActive(true);
                            NarrativeUI.ui.ReadKnot("THE_ENDING", "∞");
                            triggered = false;
                        }
                    }
                }
            }
        }

        if(triggerFinale)
        {
            timerFinale += Time.deltaTime;
            if(timerFinale > 0.0f)
            {
                cashbags.color = Color.Lerp(new Color(255, 255, 255, 0), Color.white, Utilities.Ease.cubeInOut(timerFinale / 5.0f));
                if(timerFinale > 5.0f)
                {
                    source.Stop();
                    fondu.color = Color.black;
                    fondu.enabled = true;
                    if(timerFinale > 7.0f)
                    {
                        SceneManager.LoadScene(0);
                    }
                }
            }
        }
    }

}
