using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiatedObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public GameObject reference;
    public GameObject instance = null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        if (reference != null)
        {
            instance = GameObject.Instantiate(reference, Input.mousePosition, Quaternion.identity, transform);
            Desk.d.ArmsPointAt(instance.transform);
            if (instance.name.StartsWith("KEY_OBJECT"))
                AudioManager.am.PlayRandom(AudioManager.am.keysDrag);
            else if (instance.name.StartsWith("MONEY_OBJECT"))
                AudioManager.am.PlayRandom(AudioManager.am.basicDrag);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(instance != null)
        {
            instance.transform.position = Input.mousePosition;
           
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("DROP");
        Desk.d.StopPointing();
        bool isOnExchange = false;
        foreach (Collider2D c in Physics2D.OverlapPointAll(Input.mousePosition))
        {
            Debug.Log(c);
            if (c.GetComponent<ExchangeZone>() != null)
                isOnExchange = true;
                
        }//, 8));
        if (isOnExchange
            && instance.name.StartsWith("KEY_OBJECT")
            && GhuestManager.gm.currentGhuest != null
            && GhuestManager.gm.currentEvent == GhuestEvent.CHECK_IN
            && GhuestManager.gm.checkInStep == CheckInStep.GIVE_KEY)
            {
            Desk.d.exchange.FeedObject(instance);
            GhuestManager.gm.ProceedCheckIn();
            }
        else
            Destroy(instance);

        if (instance.name.StartsWith("KEY_OBJECT"))
            AudioManager.am.PlayRandom(AudioManager.am.keysDrop);
        else if (instance.name.StartsWith("MONEY_OBJECT"))
            AudioManager.am.PlayRandom(AudioManager.am.basicDrop);
        instance = null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
