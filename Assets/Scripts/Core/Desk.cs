using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Desk : MonoBehaviour
{

    static private Desk _d;

    public Drawers drawers;
    Transform targetArms = null;
    RectTransform arms;
    float animTimer = 0.0f;
    Vector3 startArmPosition;
    public ExchangeZone exchange;
    public Phone phone;
    public AudioSource radio;

    public bool radioFade;

    public bool muteRadio = false;
    static public Desk d
    {
        get
        {
            return _d;
        }
    }

    private bool on = true;

    void Awake()
    {
        _d = this;
        Switch(true);
        exchange = GetComponentInChildren<ExchangeZone>();
        drawers = GameObject.Find("Drawers").GetComponent<Drawers>();
        arms = transform.Find("Arms").GetComponent<RectTransform>();
        phone = GameObject.FindObjectOfType<Phone>();
        radio = GameObject.Find("radio").GetComponent<AudioSource>();
    }

    public void Start()
    {
        arms.gameObject.SetActive(false);
    }

    public void Switch(bool v)
    {
        on = v;
        GetComponent<GraphicRaycaster>().enabled = on;
    }

    
    public void ArmsPointAt(Transform t)
    {
        targetArms = t;
        arms.gameObject.SetActive(true);
        arms.eulerAngles = Vector3.zero + Vector3.forward * UnityEngine.Random.Range(-20f, 20f);
        arms.position = t.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (arms.eulerAngles.z+90)), Mathf.Sin(Mathf.Deg2Rad * (arms.eulerAngles.z+90))) * -Vector3.Distance(new Vector3(580,312), transform.position);
        arms.position = t.position;
        startArmPosition = arms.position;
    }

    public void StopPointing()
    {
        targetArms = null;
        animTimer = 0.0f;
        arms.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(radioFade)
        {
            radio.volume -= 0.05f * Time.deltaTime;
        }

        if (targetArms != null)
        {
            arms.position = targetArms.position;
            /*
            float ratio = animTimer / NarrativeUI.ui.settings.armSpeed;
            if (ratio <= 1.0f)
            {
                arms.position = Vector3.Lerp(startArmPosition, targetArms.position, ratio);
                animTimer += Time.deltaTime;
            }
            else*/
        }
    }

    public void StartFadingRadio()
    {
        radioFade = true;
         
    }

    public void ActivateMoneyDrawer()
    {
        drawers.gameObject.SetActive(true);
        drawers.Activate(1);
    }

    public void ActivateKeyDrawer()
    {
        drawers.gameObject.SetActive(true);
        drawers.Activate(2);
    }

    public void ActivateMiscDrawer(int v)
    {
        drawers.gameObject.SetActive(true);
        drawers.Activate(v == 1 ? 3 : 4);
    }

    public void OnDrawGizmos()
    {
        if (targetArms != null)
        {
            Gizmos.DrawLine(startArmPosition, targetArms.position);
            //arms.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * arms.eulerAngles.z), Mathf.Sin(Mathf.Deg2Rad * arms.eulerAngles.z)) * 100f;
        }
    }

   
}
