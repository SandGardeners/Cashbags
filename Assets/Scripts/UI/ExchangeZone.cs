using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeZone : MonoBehaviour {

    GameObject content = null;
    public static GameObject keyObject;
    public static GameObject moneyObject;
	// Use this for initialization
	void Start () {
        keyObject = Resources.Load("KEY_OBJECT") as GameObject;
        moneyObject = Resources.Load("MONEY_OBJECT") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if(Physics2D.OverlapPoint(Input.mousePosition) == GetComponent<BoxCollider2D>())
            {
                Debug.Log("salut");
            }
        }
	}

    public void FeedObject(GameObject instance)
    {
        content = instance;
        content.transform.SetParent(transform, false);
        content.transform.position = transform.position;
        
    }
}
