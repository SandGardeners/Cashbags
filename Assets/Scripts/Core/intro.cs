using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class intro : MonoBehaviour {


    float timer = 0.0f;
    float desiredTimer;
    int refFontSize = 120;
    bool theEnd = false;

    Text textObject;
    string[] phrases;

    int index = 0;
    
    // Use this for initialization
    void Start ()
    {
        phrases = new string[]
        {
            "Sand Gardeners'\nPresent",
            "The Cashbags Man"
        };
        desiredTimer = NarrativeUI.ui.settings.desiredIntroTimer;
        textObject = GetComponentInChildren<Text>();
        textObject.text = phrases[0];
        textObject.color = new Color(255f,255f,255f,0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (!theEnd)
        {
            textObject.color = Color.Lerp(Color.black, Color.white, Ease.quintInOut(timer / desiredTimer));
            textObject.rectTransform.localScale = Vector3.one * Mathf.Lerp(1, 1.5f, Ease.quintOut(timer / desiredTimer));
        }
        else
        {
            GetComponent<Image>().color = Color.Lerp(Color.black, new Color(0, 0, 0, 0), Ease.expoIn(timer / desiredTimer));
            textObject.color = Color.Lerp(Color.white, new Color(255, 255, 255, 0), Ease.quartIn(timer / desiredTimer));
        }
        if(timer >= desiredTimer)
        {
            if (!theEnd)
            {
                timer = 0f;
                if (index < phrases.Length - 1)
                {
                    textObject.color = Color.black;
                    textObject.rectTransform.localScale = Vector3.one;
                    index++;
                    textObject.text = phrases[index];
                }
                else
                {
                    theEnd = true;
                    desiredTimer *= 1.25f;
                }
            }
            else
            {
                // GhuestManager.gm.PopGhuest(new Ghuest("Tutorial Timothy the Third", "TUTORIAL_GUEST"));
                GhuestManager.gm.timer = 100f;
                Destroy(gameObject);
            }

        }
       
        if(Input.GetKeyDown(KeyCode.Space))
        {
            desiredTimer = 0.1f;
        }
    }
}
