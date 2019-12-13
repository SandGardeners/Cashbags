using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looper : MonoBehaviour {

    private Vector2 offset = Vector2.zero;
    Material mat;

	// Use this for initialization
	void Start () {
        mat = GetComponentInChildren<Image>().material;
	}
	
	// Update is called once per frame
	void Update () {
        offset += Vector2.right * NarrativeUI.ui.settings.scrollingSpeed * Time.deltaTime * 0.001f;
        transform.GetChild(0).GetComponent<Image>().material.SetTextureOffset("_MainTex", offset);
    }
}
    