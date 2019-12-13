using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clock : MonoBehaviour {

    RectTransform little;
    RectTransform big;
    DraggableObject obj;
    float rotation = 0.0f;
	// Use this for initialization
	void Start () {
        little = transform.Find("littleArm").GetComponent<RectTransform>();
        big = transform.Find("bigArm").GetComponent<RectTransform>();
        obj = GetComponent<DraggableObject>();
	}

    public void Randomize()
    {
        NarrativeUI.ui.settings.clockRotationSpeed = (UnityEngine.Random.Range(-20, 20)+10)*100;
    }
	
	// Update is called once per frame
	void Update () {
        rotation += NarrativeUI.ui.settings.clockRotationSpeed * Time.deltaTime;
        big.eulerAngles = Vector3.back * rotation;
        little.eulerAngles = Vector3.back * rotation / 12;
	}
}
