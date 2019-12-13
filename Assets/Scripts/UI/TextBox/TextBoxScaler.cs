using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxScaler : MonoBehaviour {

    public Text textReader;
    public VerticalLayoutGroup group;
    public RectTransform tr;
    float curHeight;
    float tarHeight;
    float timer = 0.0f;
	// Use this for initialization
	void Start () {
        curHeight = group.preferredHeight + textReader.fontSize;
	}
	
	// Update is called once per frame
	void Update () {
        if(group.preferredHeight + textReader.fontSize != curHeight && timer == 0.0f)
        {
            timer = 0.25f;
        }
        else if(timer != 0.0f)
        {
            timer -= Time.deltaTime;
            tr.sizeDelta = new Vector2(tr.sizeDelta.x, Mathf.Lerp(curHeight, group.preferredHeight + textReader.fontSize, 1.0f - timer/0.25f));
            if(timer <= 0.0f)
            {
                timer = 0.0f;
                curHeight = group.preferredHeight + textReader.fontSize;
            }
        }
   //     Debug.Log(tr.sizeDelta = new Vector2(tr.sizeDelta.x, reader.preferredHeight+reader.fontSize));
	}
}
