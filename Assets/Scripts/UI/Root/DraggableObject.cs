using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Outline))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    bool started = false;
    Bounds? parentBounds = null;
    Rect ownRect;
    BoxCollider2D bc;
    Vector3[] points;
    public bool noDrag = false;
    public string knot = "";

    bool moved = false;
    public delegate void DownDelegate(PointerEventData eventHandler);
    public delegate void DropDelegate(PointerEventData eventHandler);

    public bool isPicture;
    public Sprite bigPicture;

    Outline o;
    public DownDelegate handlerDown;
    public void Drag()
    {

    }



    public void OnDrag(PointerEventData eventData)
    {
        if (started)
        {
            transform.position = Input.mousePosition;
            
            if (bc && !bc.bounds.Contains(transform.position))
            {
                transform.position = parentBounds.Value.ClosestPoint(transform.position);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (started && !noDrag)
        {
            started = false;
            Desk.d.StopPointing();
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        o.enabled = true;
        AudioManager.am.PlayRandom(AudioManager.am.mouseOver);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        o.enabled = false;
    }


    void onClick(PointerEventData pe)
    {

    }
    // Use this for initialization
    void Start()
    {
        points = new Vector3[2];
        o = GetComponent<Outline>();
        if (o == null)
            o = gameObject.AddComponent<Outline>();
        o.effectColor = Color.white;
        o.effectDistance = new Vector2(2f, 2f);
        o.enabled = false;
        // Instantiate the delegate.

        // Call the delegate.

    }
    void EvaluatePoints()
    {
        Vector2 tp = new Vector2(transform.position.x, transform.position.y);

     //   points[0] = tp + ownRect.size / 4f;
        points[0] = tp - ownRect.size / 4f;
    //    points[1] = tp + new Vector2(-ownRect.width, ownRect.height) / 4f;
        points[1] = tp + new Vector2(ownRect.width, -ownRect.height) / 4f;
    }
    // Update is called once per frame
    void Update()
    {
        EvaluatePoints();
        if(!noDrag && !started && bc)
        {
            foreach (Vector3 p in points)
            {
                if (!bc.bounds.Contains(p))
                {
                    transform.position -= (p - parentBounds.Value.ClosestPoint((p)));// bc.transform.position).normalized*(ownRect.width/2+ownRect.height/2)/2);
                    break;
                    //EvaluatePoints();
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (points != null)
        {
            foreach (Vector2 p in points)
            {
                Gizmos.DrawSphere(p, 10.0f);
            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!started)
        {
            AudioManager.am.PlayRandom(AudioManager.am.click);

            if (isPicture)
            {
                PictureShower.ps.Display(gameObject);
            }
            else if (knot != string.Empty)
            {
                if (knot == "DEBUGBOOK")
                {
                    if (GhuestManager.gm.currentGhuest != null
                        && GhuestManager.gm.currentEvent == GhuestEvent.CHECK_IN
                        && GhuestManager.gm.checkInStep == CheckInStep.WRITE_NAME)
                    {
                        AudioManager.am.PlayRandom(AudioManager.am.writing);
                        GhuestManager.gm.ProceedCheckIn();
                    }
                }
                else if (knot == "DEBUGRADIO")
                {
                    if (!Desk.d.radioFade)
                    {
                        Desk.d.muteRadio = !Desk.d.muteRadio;
                        if (GetComponent<AudioSource>().volume == NarrativeUI.ui.settings.volumeRadio)
                        {
                            AudioManager.am.PlayRadio(false);
                            GetComponent<AudioSource>().volume = 0;
                        }
                        else
                        {
                            AudioManager.am.PlayRadio(true);
                            GetComponent<AudioSource>().volume = NarrativeUI.ui.settings.volumeRadio;
                        }
                    }
                }
                else if (knot == "DEBUGPHONE")
                {
                    if (Desk.d.phone.isRinging)
                        Desk.d.phone.PickUp();
                }
                else if (knot == "KEYDRAWERDEBUG")
                {
                    Desk.d.ActivateKeyDrawer();
                }
                else if (knot == "MONEYDRAWERDEBUG")
                {

                    Desk.d.ActivateMoneyDrawer();
                }
                else if (knot == "CLOCKDEBUG")
                {
                    GetComponent<Clock>().Randomize();
                }
                else if (knot == "MESSAGEDEBUG")
                {
                    if (BinaryRiver.br.isWaiting)
                    {
                        AudioManager.am.PlayRandom(AudioManager.am.clickMail);
                        BinaryRiver.br.OpenMail();
                    }
                }
                else if (knot == "MISCFIRSTDRAWERDEBUG")
                {
                    Desk.d.ActivateMiscDrawer(2);
                }
                else if (knot == "MISCSECONDDRAWERDEBUG")
                {
                    Desk.d.ActivateMiscDrawer(1);
                }
                else
                {
                    if (knot == "ACTIVABLE_BELL")
                        AudioManager.am.PlayBell();
                    NarrativeUI.ui.SwitchActive(true);
                    NarrativeUI.ui.ReadKnot(knot, "");
                }
            }
        }
        
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //handlerDown(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!noDrag)
        {
            Desk.d.ArmsPointAt(transform);
            Debug.Log("salut");
            ownRect = GetComponent<RectTransform>().rect;



            bc = transform.parent.GetComponent<BoxCollider2D>();
            if (bc != null)
            {
                parentBounds = bc.bounds;
            }
            started = true;
        }
    }
}
