using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.UI;

public class Item2d : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public  Item item;
    public int nb;
    Vector3 startPos;
    public GridSlot lastSlot;
    public bool isCollectible;

    Outline o;

    public void Start()
    {
        o = GetComponent<Outline>();
        if (o == null)
            o = gameObject.AddComponent<Outline>();
        o.effectColor = Color.white;
        o.effectDistance = new Vector2(2f, 2f);
        o.enabled = false;
    }

    //public ItemInfo info;



    public void OnBeginDrag(PointerEventData eventData)
    {
        lastSlot = transform.parent.GetComponent<GridSlot>();
        if (gameObject.name.StartsWith("KEY_OBJECT"))
        {
            Desk.d.ActivateKeyDrawer();
            transform.SetParent(Desk.d.transform, false);
            AudioManager.am.PlayRandom(AudioManager.am.keysDrag);

        }
        else if (gameObject.name.StartsWith("MONEY_OBJECT"))
        {
            Desk.d.ActivateMoneyDrawer();
            AudioManager.am.PlayRandom(AudioManager.am.basicDrag);
            transform.SetParent(Desk.d.transform, false);
        }
        startPos = transform.position;
        // ici on peut actualiser les stats
        //transform.SetParent(lastSlot.transform.parent,true);
        transform.position = eventData.position;
        if (lastSlot != null)
        {
            //if (lastSlot.localisation != string.Empty && item is Equipement)
            {
                //    item.removeStats();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit hit;
        transform.position = startPos;
        if (transform.parent.GetComponent<GridSlot>() != null)
        {
            // je suis sur un slot valide.
        }
        else if (GhuestManager.gm.currentEvent == GhuestEvent.CHECK_IN
            && GhuestManager.gm.checkInStep == CheckInStep.GET_MONEY
            && Desk.d.drawers.boundsDrawers.Contains(Input.mousePosition - Desk.d.drawers.transform.position))
        {
            GhuestManager.gm.ProceedCheckIn();
            Destroy(this.gameObject);
        }
        else if (Desk.d.drawers.boundsDrawers.Contains(Input.mousePosition - Desk.d.drawers.transform.position)
            && isCollectible)
        {
            Desk.d.drawers.AddCollectibleItem(this.gameObject);
        }
        else
        {
            transform.SetParent(lastSlot.transform, false);
            Desk.d.drawers.Activate(0);
        }
        GridSlot gridSlot = transform.parent.GetComponent<GridSlot>();
        if (gridSlot != null)
        {
            //   if (gridSlot.localisation !=string.Empty && item is Equipement)
            {
                //       item.applyStats();
            }
        }


        if (gameObject.name.StartsWith("KEY_OBJECT"))
            AudioManager.am.PlayRandom(AudioManager.am.keysDrop);
        else if (gameObject.name.StartsWith("MONEY_OBJECT"))
            AudioManager.am.PlayRandom(AudioManager.am.basicDrop);



        // if OnGround tombe au sol.
        // if New case : deplace
        // else Retour case depart.

        // ici on peut actualiser les stats
        // On pourrait faire un RayCastAll pour vérifier ce qui est en dessous et faire des trucs
        // plus précis 
        // if OnGround, Conversion. 
        // if On
        // motherObject.popOnGround()
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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
}
